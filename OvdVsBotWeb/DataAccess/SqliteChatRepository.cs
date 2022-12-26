using Microsoft.EntityFrameworkCore;
using OvdVsBotWeb.Models.Data;

namespace OvdVsBotWeb.DataAccess
{
    public class SqliteChatRepository : IReadWriter<Chat, string>
    {
        private readonly OvdDbContext _dbContext;

        public SqliteChatRepository(IServiceScopeFactory factory)
        {
            _dbContext = factory
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<OvdDbContext>();
            //_dbContext.ChangeTracker.Clear();
            //_dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add(Chat entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public bool Any() => throw new NotImplementedException();

        public Chat Get(string id) => _dbContext
            .Chats
            .AsQueryable()
            .FirstOrDefault(c => c.Id == id);

        public Chat Get(Func<Chat> filter) => throw new NotImplementedException();

        public IEnumerable<Chat> GetAll() => _dbContext
            .Chats
            .AsNoTracking();

        //public IEntity<TId> Get(Func<IEntity<TId>> filter) => _dict.Where(x => filter(x));

        public void Remove(Chat entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.SaveChanges();
        }

        public void Remove(string id)
        {
            //var entity = Get(id);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.SaveChanges();
        }

        public void Update(Chat entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.SaveChanges();
        }
    }
}
