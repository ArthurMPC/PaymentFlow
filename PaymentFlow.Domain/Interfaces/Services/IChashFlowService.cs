using PaymentFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Domain.Interfaces.Services
{
    public interface ICashFlowService
    {

        List<CashFlow> GetCashFow(List<Transaction> transactionList);

        decimal GenerateBalance(decimal totalEntries, decimal totalExits, decimal totalChages);

    }
}
