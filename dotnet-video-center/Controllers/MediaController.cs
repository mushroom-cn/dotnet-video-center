using GMM7.Jobs;
using GMM7.Models;
using GMM7.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace GMM7.Controllers;

[ApiController]
[Route("/v1/[controller]")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
// [Produces("application/json")]
public class MediaController : ControllerBase
{


    public MediaController(
    IEntityService<Media> mediaService)
    {
    }

    /// <summary>Get media description by media id.</summary>
    /// <param name="id">Media id</param>
    /// <return>Media description</return>
    /// <response code="200">Returns the media description. </response>
    /// <response code="404">Media description not found. </response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Media GetMedia([FromServices] IEntityService<Media> MediaService, string id)
    {
        return MediaService.GetEntitiesById(new List<string> { id }).FirstOrDefault<Media>();
    }

    /// <summary>Get media description collection by media ids.</summary>
    /// <param name="ids">A group of media id</param>
    /// <return>Media description collection</return>
    /// <response code="200">Returns the media description. </response>
    /// <response code="404">Media description not found. </response>
    [HttpGet("ids")]
    public IEnumerable<Media> GetMediasByIds([FromServices] IEntityService<Media> MediaService, [FromQuery] IEnumerable<string> ids)
    {
        return MediaService.GetEntitiesById(ids);
    }

    /// <summary>Get media description collection by media names. support flex search</summary>
    /// <param name="name">Media name</param>
    /// <return>Media description collection</return>
    /// <response code="200">Returns the media description. </response>
    /// <response code="404">Media description not found. </response>
    [HttpGet("name/{name}")]
    public IEnumerable<Media> GetMediasByName([FromServices] IEntityService<Media> MediaService, string name)
    {
        return MediaService.GetEntitiesByName(name);
    }

    /// <summary>Modify media description.</summary>
    /// <param name="id">Media id</param>
    /// <param name="media">Media entity</param>
    /// <return>Modified media</return>
    [HttpPut("{id:guid}")]

    public Media ModifyMedia([FromServices] IEntityService<Media> MediaService, string id, [FromBody] Media media)
    {
        return MediaService.ModifyEntities(new List<Media> { media }).FirstOrDefault();
    }

    /// <summary>Batch modify media description.</summary>
    /// <param name="medias">Media entity collection.</param>
    /// <return>Modified media collection.</return>
    [HttpPut("ids")]
    public IEnumerable<Media> ModifyMedias([FromServices] IEntityService<Media> MediaService, [FromBody] IEnumerable<Media> medias)
    {
        return MediaService.ModifyEntities(medias);
    }

    /// <summary>Batch create media description.</summary>
    /// <param name="medias">Media entity collection.</param>
    /// <return>Created media collection.</return>
    [HttpPost("")]
    public IEnumerable<Media> CreateMedias([FromServices] IEntityService<Media> MediaService, [FromBody] IEnumerable<Media> medias)
    {
        return MediaService.CreateEntities(medias);
    }

    /// <summary>Delete media description.</summary>
    /// <param name="id">Media id.</param>
    /// <return>void</return>
    [HttpDelete("{id:guid}")]
    public void DeleteMedia(
         [FromServices] IEntityService<Media> MediaService,
         [FromServices] IConfiguration configuration,
         [FromServices] ILogger<MediaController> logger,
         string id
    )
    {
        DeleteMediasCore(MediaService, configuration, logger, new List<string> { id });
    }

    /// <summary>Batch delete media description.</summary>
    /// <param name="ids">A group of media id.</param>
    /// <return>void</return>
    [HttpDelete()]
    public void DeleteMedias(
        [FromServices] IEntityService<Media> MediaService,
        [FromServices] IConfiguration configuration,
        [FromServices] ILogger<MediaController> logger,
        [FromBody] IEnumerable<string> ids
    )
    {
        DeleteMediasCore(MediaService, configuration, logger, ids);
    }

    private void DeleteMediasCore(
         IEntityService<Media> MediaService,
         IConfiguration configuration,
         ILogger<MediaController> logger,
         IEnumerable<string> ids
    )
    {
        var entries = MediaService.GetEntitiesById(ids);
        MediaService.DeleteEntitiesByIds(ids);
        var searchedFiles = Directory.GetFiles(Path.GetFullPath(Path.Combine(configuration["FFMpeg:Target"])), "*.*")
      .Where(file => entries.Any(entry => file.Contains(entry.Url)));
        foreach (var file in searchedFiles)
        {
            System.IO.File.Delete(file);
            logger.LogInformation($"Delete file {file}");
        }
    }

    /// <summary>Unbind tag with media.</summary>
    /// <param name="mediaId">Media id.</param>
    /// <param name="tagId">Tag id.</param>
    /// <return>void</return>
    [HttpDelete("{mediaId:guid}/tag/{tagId:guid?}")]
    public void DeleteMediaTag([FromServices] IRelationService<MediaTag> mediaTagService,
    string mediaId,
    string tagId,
    [FromBody] IEnumerable<string> tagIds
    )
    {
        mediaTagService.Delete(mediaId, new List<string> { tagId }.Concat(tagIds ?? new List<string>()).Where(p => !string.IsNullOrEmpty(p)));
    }

    /// <summary>Bind tag with media.</summary>
    /// <param name="mediaId">Media id.</param>
    /// <param name="tagId">Tag id.</param>
    /// <return>void</return>
    [HttpPost("{mediaId}/tag/{tagId?}")]
    public void CreateMediaTag(
        [FromServices] IRelationService<MediaTag> mediaTagService,
        string mediaId, string tagId, [FromBody] IEnumerable<string> tagIds)
    {
        mediaTagService.Create(mediaId, new List<string> { tagId }.Concat(tagIds ?? new List<string>()).Where(p => !string.IsNullOrEmpty(p)));
    }

    [HttpPut("convert/{id}")]
    public string Convert(
        [FromServices] IFFfmpegConverter ffmpeg,
        [FromServices] IEntityService<Media> MediaService,
        [FromServices] IConfiguration configuration,
         string id
    )
    {
        var media = MediaService.GetEntitiesById(new List<string> { id }).FirstOrDefault();
        var targetName = $"{Guid.NewGuid().ToString()}.m3u8";
        var target = Path.Combine(configuration["FFmpeg:Target"], targetName);
        var result = ffmpeg.StartConvertTask(media.Path, configuration["FFmpeg:Target"]);
        result.OnFFmpegConvertResultCompleteHandler += (sender, args) =>
        {
            media.Url = targetName;
            MediaService.ModifyEntities(new List<Media> { media });
        };
        return result.TaskId;
    }

    [HttpPost("scan")]
    public async void ScanFile([FromServices] IFileScannerService fileScannerService, [FromServices] IConfiguration configuration)
    {
        var targetDir = configuration["FFmpeg:Target"];
        var sourceDir = Path.GetFullPath(configuration["FFmpeg:Source"]);
        var searchOptions = configuration["FFmpeg:SearchOption"].Split(",");
        await fileScannerService.Scan(sourceDir, targetDir, searchOptions);
    }
}
