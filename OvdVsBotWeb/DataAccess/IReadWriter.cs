namespace OvdVsBotWeb.DataAccess
{
    public interface IReadWriter<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        public TEntity Get(TId id);
        public TEntity Get(Func<TEntity> filter);
        public IEnumerable<TEntity> GetAll();
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public void Remove(TEntity entity);
        public void Remove(TId id);
    }
}
