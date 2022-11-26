using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using static Dapper.SqlMapper;

namespace OvdVsBotWeb.DataAccess
{
    public class MemoryRepository<TId> : IReadWriter<TId>
    {
        private readonly ConcurrentDictionary<TId, IEntity<TId>> _dict = new();

        public void Add(IEntity<TId> entity) => _dict[entity.Id] = entity;

        public IEntity<TId> Get(TId id) => _dict[id];

        public IEntity<TId> Get(Func<IEntity<TId>> filter) => throw new NotImplementedException();

        public IEnumerable<IEntity<TId>> GetAll() => _dict.Values;

        //public IEntity<TId> Get(Func<IEntity<TId>> filter) => _dict.Where(x => filter(x));

        public void Remove(IEntity<TId> entity) => _dict.Remove(entity.Id, out _);

        public void Remove(TId id) => _dict.Remove(id, out _);

        public void Update(IEntity<TId> entity)
            => _dict[entity.Id] = entity;
    }

}
