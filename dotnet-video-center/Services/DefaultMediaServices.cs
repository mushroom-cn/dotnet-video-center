using GMM7.Models;
using Microsoft.EntityFrameworkCore;

namespace GMM7.Services
{

    public class DefaultMediaServices : IEntityService<Media>
    {
        private readonly IDBContext Context;
        private readonly ILogger<DefaultMediaServices> Logger;
        public DefaultMediaServices(IDBContext context, ILogger<DefaultMediaServices> logger)
        {
            Context = context;
            Logger = logger;
        }

        IDBContext IEntityService<Media>.Context => Context;

        public int DeleteEntitiesByIds(IEnumerable<string> ids)
        {
            if (ids?.Count() < 1)
            {
                return 0;
            }
            var guids = ids.Select((i) => Guid.Parse(i));
            var entries = from media in Context.Medias
                          where guids.Contains(media.Id)
                          select media;
            if (entries.Count() < 1)
            {
                return 0;
            }
            Context.RemoveRange(entries);
            return Context.SaveChanges();
        }

        public IEnumerable<Media> GetEntitiesById(IEnumerable<string> ids)
        {
            if (ids.Distinct().Count() < 1)
            {
                return (from media in Context.Medias
                        select media).ToList();
            }
            var guids = ids.Select((i) => Guid.Parse(i));
            var result = from media in Context.Medias
                         where guids.Contains(media.Id)
                         select media;
            return result.ToList();
        }

        public IEnumerable<Media> GetEntitiesByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<Media> { };
            }
            var result = from media in Context.Medias
                         where media.Name.Contains(name)
                         select media;
            return result.ToList<Media>();
        }

        public IEnumerable<Media> CreateEntities(IEnumerable<Media> t)
        {
            try
            {

                Context.Medias.AddRange(t);
                Context.SaveChanges();
                return t;
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }
            return null;
        }

        public IEnumerable<Media> ModifyEntities(IEnumerable<Media> t)
        {
            if (t?.Count() < 1)
            {
                return t;
            }
            Context.Medias.UpdateRange(t);
            Context.SaveChanges();
            return t;
        }

    }
}