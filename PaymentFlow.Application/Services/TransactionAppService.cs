using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces.Application;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        ITransactionService _transactionService;
        IQueueService _queueService;

        public TransactionAppService(ITransactionService transactionService, IQueueService queueService)
        {
            _transactionService = transactionService;
            _queueService = queueService;
        }

        public async Task Add(Transaction entity)
        {
            await _transactionService.Add(entity);
        }

        public async Task QueueDailyTransactionWithPositionValidation(Transaction transaction)
        {
            List<Transaction> transactionList = await _transactionService.GetDailyTransaction(transaction.LaunchDate);

            decimal balance = transactionList.Where(y => y.Kind == Domain.Enums.TransactionKind.Entrie).Sum(x => x.Value)
                - transactionList.Where(y => y.Kind == Domain.Enums.TransactionKind.Exit).Sum(x => x.Value) 
                - transactionList.Sum(x => x.Charges);

            if (transaction.Kind == Domain.Enums.TransactionKind.Entrie)
                balance += transaction.Value;
            else
                balance -= transaction.Value;
            

            if ((balance) < -20000m)
                throw new Exception("Overdraft was overdue");

            _queueService.QueueMessage(nameof(transaction) ,transaction);
        }

        public async Task Delete(string id)
        {
            await _transactionService.Delete(id);
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _transactionService.GetAll();
        }

        public async Task<Transaction> GetById(string id)
        {
            return await _transactionService.GetById(id);
        }

        public List<Transaction> GetMonthTransaction()
        {
            return _transactionService.GetMonthTransaction();
        }
    }
}
