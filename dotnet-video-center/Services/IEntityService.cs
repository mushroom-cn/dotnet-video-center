namespace GMM7.Services
{
    public interface IEntityService<T>
    {
        protected IDBContext Context { get; }

        public IEnumerable<T> GetEntitiesById(IEnumerable<string> ids);
        public IEnumerable<T> GetEntitiesByName(string name);
        public IEnumerable<T> CreateEntities(IEnumerable<T> t);
        public IEnumerable<T> ModifyEntities(IEnumerable<T> t);
        public int DeleteEntitiesByIds(IEnumerable<string> ids);
    }

    public interface IRelationService<T>
    {
        public void Delete(string masterId, IEnumerable<string> relatedIds);
        public void Create(string masterId, IEnumerable<string> relatedIds);
    }
}