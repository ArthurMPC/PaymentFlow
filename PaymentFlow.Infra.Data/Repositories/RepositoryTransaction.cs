using MongoDB.Bson;
using MongoDB.Driver;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Infra.Data.Repositories
{
    public class RepositoryTransaction : RepositoryBase<Transaction>, IRepositoryTransaction
    {
        public RepositoryTransaction(IMongoDatabase context) : base(context)
        {
        }

        public async Task<List<Transaction>> GetDailyTransactions(DateTime transactionDate)
        {
            FilterDefinition<Transaction> filter =
                new FilterDefinitionBuilder<Transaction>().Eq(x => x.LaunchDate, new BsonDateTime(transactionDate));
            return await base.DbSet.Find(filter).ToListAsync();
        }

        public List<Transaction> GetMonthTransactions()
        {
            FilterDefinition<Transaction> filter = 
                new FilterDefinitionBuilder<Transaction>().Gte(x => x.LaunchDate, new BsonDateTime(DateTime.Now)) & 
                new FilterDefinitionBuilder<Transaction>().Lte(y => y.LaunchDate, new BsonDateTime(DateTime.Now.AddDays(30)));
            return base.DbSet.Find(filter).ToList();
        }
    }
}
