using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Infra.Data.Context
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>();

    }
}
