namespace OvdVsBotWeb.DataAccess
{
    public interface IReadWriter<TId>
    {
        public IEntity<TId> Get(TId id);
        public IEntity<TId> Get(Func<IEntity<TId>> filter);
        public IEnumerable<IEntity<TId>> GetAll();
        public void Add(IEntity<TId> entity);
        public void Remove(IEntity<TId> entity);
        public void Remove(TId id);
    }
}
