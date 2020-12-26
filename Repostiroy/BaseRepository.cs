using Entity;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repostiroy
{
    public class BaseRepository<T> where T : BaseEntity
    {
        private IMongoCollection<T> mongoCollection;
        private MongoClient client;
        
        string database;
        string collection = "";
        
        public BaseRepository(string connectionString, string database, string collection)
        {
            client = new MongoClient(connectionString);
            this.database = database;
            this.collection = collection;
            var db = client.GetDatabase(database);
            mongoCollection = db.GetCollection<T>(collection);
        }

        public MongoClient Client() => client;

        public IMongoCollection<T> Collection() => mongoCollection;

        public IMongoQueryable<T> GetAll()
        {
            return mongoCollection.AsQueryable();
        }

        public IMongoQueryable<T> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return GetAll().Where(expression);
        }

        public async Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return await GetAll().Where(expression).AnyAsync();
        }

        public async Task<T> GetByIdAsync(string Id)
        {
            return await mongoCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt= DateTime.UtcNow;


            await mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        
        public virtual async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt= DateTime.UtcNow;
            await mongoCollection.ReplaceOneAsync(m => m.Id == entity.Id, entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await this.Collection().FindOneAndDeleteAsync(x => x.Id == id);
        }
    }

}
