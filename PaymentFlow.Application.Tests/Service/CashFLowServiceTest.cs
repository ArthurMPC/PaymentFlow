using PaymentFlow.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using PaymentFlow.Application.Services;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Enums;

namespace PaymentFlow.Application.Tests.Service
{
    public class CashFlowServiceTest
    {

        [Fact]
        public void ApplyInterest_Tests()
        {
            CashFlowService cashFlow = new CashFlowService();

            decimal balance = cashFlow.ApplyInterest(-1000m, -100m);

            Assert.Equal(0.83m, balance);
        }

        [Fact]
        public void GenerateBalance_Tests()
        {

            CashFlowService cashFlow = new CashFlowService();

            decimal balance = cashFlow.GenerateBalance(1000m, 500m, 500m);

            Assert.Equal(0m, balance);
            
        }

        [Fact]
        public void GetCashflowService_Tests()
        {
            List<Transaction> transactions = new List<Transaction>();

            transactions.Add(new Transaction()
            {
                Bank = "ContaStone",
                Account = "001",
                Value = 102.75m,
                Kind = TransactionKind.Entrie,
                Description = "Teste",
                TransactionDate = DateTime.Now,
                Document = "0123456789",
                Charges = 2.75m,
                LaunchDate = DateTime.Now.AddDays(3)

            });

            transactions.Add(new Transaction()
            {
                Bank = "ContaStone",
                Account = "001",
                Value = 5m,
                Kind = TransactionKind.Exit,
                Description = "Teste",
                TransactionDate = DateTime.MaxValue,
                Document = "0123456789",
                Charges = 0m,
                LaunchDate = DateTime.Now.AddDays(6)

            });

            CashFlowService cashFlow = new CashFlowService();

            List<CashFlow> cashFlows = cashFlow.GetCashFow(transactions);

            Assert.NotNull(cashFlows);
            Assert.Equal(2, cashFlows.Count);
            Assert.Equal(100m, cashFlows[0].Total);
            Assert.Equal(-4.9585M, cashFlows[1].Total);
            Assert.Equal(100m, cashFlows[0].PositionOfTheDay);
            Assert.Equal(-104.9585M, cashFlows[1].PositionOfTheDay);
        }

    }
}
