namespace OvdVsBotWeb.DataAccess
{
    public interface IReadWriter<T, TId>
    {
        public T Get(TId id);
        public T Get(Func<T> filter);
        public void Add(T entity);
        public void Remove(T entity);
        public void Remove(TId id);
    }
}
