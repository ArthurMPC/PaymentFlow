using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace PaymentFlow.Infra.Data.Context
{
    public class MongoDbContext : IMongoContext
    {
        public IMongoDatabase MongoDBConnection = null;

        public MongoDbContext(IMongoDatabase mongoDatabase)
        {
            MongoDBConnection = mongoDatabase;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return MongoDBConnection.GetCollection<T>(typeof(T).Name);
        }

    }
}
