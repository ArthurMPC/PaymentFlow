using MongoDB.Driver;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Infra.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {

        protected readonly IMongoDatabase _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        public RepositoryBase(IMongoDatabase context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Add(TEntity entity)
        {
            await DbSet.InsertOneAsync(entity);
        }

        public async Task Add(IEnumerable<TEntity> entities)
        {
            await DbSet.InsertManyAsync(entities);
        }

        public async Task Delete(string id)
        {
            await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        public Task<List<TEntity>> GetAll()
        {
            return DbSet.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        public Task<TEntity> GetById(string id)
        {
            return DbSet.Find(Builders<TEntity>.Filter.Eq(field: "_id", value: id)).FirstAsync();
        }
    }
}
