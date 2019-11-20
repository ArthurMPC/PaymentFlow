using PaymentFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Repositories
{
    public interface IRepositoryTransaction : IRepositoryBase<Transaction>
    {
        List<Transaction> GetMonthTransactions();

        Task<List<Transaction>> GetDailyTransactions(DateTime transactionDate);
    }
}
