using PaymentFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Application
{
    public interface ITransactionAppService : IAppServiceBase<Transaction>
    {

        List<Transaction> GetMonthTransaction();

        Task QueueDailyTransactionWithPositionValidation(Transaction transaction);

    }
}
