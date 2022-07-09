using GMM7.Models;
namespace GMM7.Services
{

    public class DefaultTagServices : IEntityService<Tag>
    {
        private readonly IDBContext Context;
        public DefaultTagServices(IDBContext context)
        {
            Context = context;
        }

        IDBContext IEntityService<Tag>.Context => Context;

        public int DeleteEntitiesByIds(IEnumerable<string> ids)
        {
            if (ids?.Count() < 1)
            {
                return 0;
            }
            var guids = ids.Select((i) => Guid.Parse(i));
            var entries = from tag in Context.Tags
                          where guids.Contains(tag.Id)
                          select tag;
            if (entries.Count() < 1)
            {
                return 0;
            }
            Context.RemoveRange(entries);
            return Context.SaveChanges();
        }

        public IEnumerable<Tag> GetEntitiesById(IEnumerable<string> ids)
        {
            if (ids.Distinct().Count() < 1)
            {
                return (from media in Context.Tags
                        select media).ToList();
            }
            var guids = ids.Select((i) => Guid.Parse(i));
            var result = from media in Context.Tags
                         where guids.Contains(media.Id)
                         select media;
            return result.ToList<Tag>();
        }

        public IEnumerable<Tag> GetEntitiesByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<Tag> { };
            }
            var result = from media in Context.Tags
                         where media.Name.Contains(name)
                         select media;
            return result.ToList<Tag>();
        }

        public IEnumerable<Tag> CreateEntities(IEnumerable<Tag> t)
        {
            Context.Tags.AddRange(t);
            Context.SaveChanges();
            return t;
        }
        public IEnumerable<Tag> ModifyEntities(IEnumerable<Tag> t)
        {
            if (t?.Count() < 1)
            {
                return t;
            }
            Context.Tags.UpdateRange(t);
            Context.SaveChanges();
            return t;
        }
    }
}