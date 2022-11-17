﻿using Microsoft.EntityFrameworkCore;

namespace OvdVsBotWeb.DataAccess
{
    public class SqliteChatRepository : IReadWriter<string>
    {
        private readonly OvdDbContext _dbContext;

        public SqliteChatRepository(IServiceScopeFactory factory)
        {
            _dbContext = factory.CreateScope().ServiceProvider.GetRequiredService<OvdDbContext>();
        }

        public void Add(IEntity<string> entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        public bool Any()
        {
            throw new NotImplementedException();
        }

        public IEntity<string> Get(string id) => _dbContext.Chats.FirstOrDefault(c => c.Id == id);

        public IEntity<string> Get(Func<IEntity<string>> filter) => throw new NotImplementedException();

        public IEnumerable<IEntity<string>> GetAll() => _dbContext.Chats.ToList();

        //public IEntity<TId> Get(Func<IEntity<TId>> filter) => _dict.Where(x => filter(x));

        public void Remove(IEntity<string> entity) => _dbContext.Remove(entity);

        public void Remove(string id) => _dbContext.Chats.Remove(_dbContext.Chats.FirstOrDefault(c => c.Id == id));
    }

}
