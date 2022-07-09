using System.ComponentModel.DataAnnotations;
using GMM7.Models;
using GMM7.Services;
using Microsoft.AspNetCore.Mvc;

namespace GMM7.Controllers;

[ApiController]
[Route("/v1/[controller]")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
// [Produces("application/json")]
public class SettingController : ControllerBase
{


    public SettingController(
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
    [HttpPost("create")]
    public IEnumerable<Media> CreateMedias([FromServices] IEntityService<Media> MediaService, [FromBody] IEnumerable<Media> medias)
    {
        return MediaService.CreateEntities(medias);
    }

    /// <summary>Delete media description.</summary>
    /// <param name="id">Media id.</param>
    /// <return>void</return>
    [HttpDelete("{id:guid}")]
    public void DeleteMedias([FromServices] IEntityService<Media> MediaService, string id)
    {
        MediaService.DeleteEntitiesByIds(new List<string> { id });
    }

    /// <summary>Batch delete media description.</summary>
    /// <param name="ids">A group of media id.</param>
    /// <return>void</return>
    [HttpDelete()]
    public void DeleteMedias([FromServices] IEntityService<Media> MediaService, [FromBody] IEnumerable<string> ids)
    {
        MediaService.DeleteEntitiesByIds(ids);
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

}
