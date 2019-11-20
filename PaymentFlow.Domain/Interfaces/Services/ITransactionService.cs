using PaymentFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Services
{
    public interface ITransactionService : IServiceBase<Transaction>
    {

        List<Transaction> GetMonthTransaction();

        Task<List<Transaction>> GetDailyTransaction(DateTime transactionDate);

    }
}
