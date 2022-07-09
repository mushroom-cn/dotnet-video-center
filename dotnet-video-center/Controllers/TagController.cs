using GMM7.Models;
using GMM7.Services;
using Microsoft.AspNetCore.Mvc;

namespace GMM7.Controllers;

[ApiController]
[Route("/v1/[controller]")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
[Produces("application/json")]
public class TagController : ControllerBase
{
    public TagController()
    {
    }
    /// <summary>Get Tag info by tag id </summary>
    /// <param name="id">Tag id</param>
    /// <return>Tag info</return>
    [HttpGet("id/{id}")]
    public Tag GetTag([FromServices] IEntityService<Tag> TagService, string id)
    {
        return TagService.GetEntitiesById(new List<string> { id }).FirstOrDefault<Tag>();
    }

    /// <summary>Get Tag info collection by a group of tag id </summary>
    /// <param name="ids">A group of tag id</param>
    /// <return>A collection of Tag</return>
    [HttpGet()]
    public IEnumerable<Tag> GetTagsByIds([FromServices] IEntityService<Tag> TagService, [FromBody] IEnumerable<string> ids)
    {
        return TagService.GetEntitiesById(ids);
    }

    /// <summary>Get Tag info collection by tag name</summary>
    /// <param name="name">Tag name</param>
    /// <return>void</return>
    [HttpGet("name/{name}")]
    public IEnumerable<Tag> GetTagsByName([FromServices] IEntityService<Tag> TagService, string name)
    {
        return TagService.GetEntitiesByName(name);
    }

    /// <summary>Modify Tag</summary>
    /// <param name="tags">A group of tag</param>
    /// <return>Modified Tags</return>
    [HttpPut()]
    public IEnumerable<Tag> ModifyTags([FromServices] IEntityService<Tag> TagService, [FromBody] IEnumerable<Tag> tags)
    {
        return TagService.ModifyEntities(tags);
    }

    /// <summary>Create Tag</summary>
    /// <param name="tags">A group of tag</param>
    /// <return>Created Tags</return>
    [HttpPost()]
    public IEnumerable<Tag> CreateTags([FromServices] IEntityService<Tag> TagService, [FromBody] IEnumerable<Tag> tags)
    {
        return TagService.CreateEntities(tags);
    }

    /// <summary>Delete Tag by a group od id</summary>
    /// <param name="ids">A group of tag id</param>
    /// <return>void</return>
    [HttpDelete()]
    public void DeleteTags([FromServices] IEntityService<Tag> TagService, [FromBody] IEnumerable<string> ids)
    {
        TagService.DeleteEntitiesByIds(ids);
    }
}
