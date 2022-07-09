using GMM7.Models;

namespace GMM7.Services
{

    public class DefaultMediaTagServices : IRelationService<MediaTag>
    {
        private readonly IDBContext Context;
        private readonly IEntityService<Media> MediaService;
        private readonly IEntityService<Tag> TagService;

        public DefaultMediaTagServices(IDBContext context,
        IEntityService<Media> mediaService,
        IEntityService<Tag> tagService)
        {
            Context = context;
            MediaService = mediaService;
            TagService = tagService;
        }

        public void Create(string masterId, IEnumerable<string> relatedIds)
        {
            if (string.IsNullOrEmpty(masterId) || relatedIds.Count() < 1)
            {
                return;
            }
            var medias = MediaService.GetEntitiesById(new List<string> { masterId });
            if (medias.Count() < 1)
            {
                return;
            }
            Context.MediaTags.AddRange(from relatedId in relatedIds
                                       select new MediaTag
                                       {
                                           MediaId = Guid.Parse(masterId),
                                           TagId = Guid.Parse(relatedId),
                                       });
            Context.SaveChanges();
        }

        public void Delete(string masterId, IEnumerable<string> relatedIds)
        {
            if (string.IsNullOrEmpty(masterId) || relatedIds.Count() < 1)
            {
                return;
            }
            Context.MediaTags.RemoveRange(from relatedId in relatedIds
                                          select new MediaTag
                                          {
                                              MediaId = Guid.Parse(masterId),
                                              TagId = Guid.Parse(relatedId),
                                          });
            Context.SaveChanges();
        }
    }
}