using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Service
{
    public class TransactionService : ServiceBase<Transaction>, ITransactionService
    {
        private readonly IRepositoryTransaction _repositoryTransaction;

        public TransactionService(IRepositoryTransaction repository) : base(repository)
        {
            _repositoryTransaction = repository;
        }

        public async Task<List<Transaction>> GetDailyTransaction(DateTime transactionDate)
            => await _repositoryTransaction.GetDailyTransactions(transactionDate);

        public List<Transaction> GetMonthTransaction()
            => _repositoryTransaction.GetMonthTransactions();
    }
}
