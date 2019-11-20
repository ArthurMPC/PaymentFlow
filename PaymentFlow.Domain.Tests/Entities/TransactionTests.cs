using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentFlow.Domain.Tests.Entities
{
    public class TransactionTests
    {
        [Fact]
        public void Properties_Tests()
        {

            Transaction transaction = new Transaction()
            {
                Bank = "ContaStone",
                Account = "001",
                Value = 100.5m,
                Kind = TransactionKind.Entrie,
                Description = "Teste",
                TransactionDate = DateTime.MaxValue,
                Document = "0123456789",
                Charges = 2.75m,
                LaunchDate = DateTime.MinValue

            };

            Assert.Null(transaction.Id);
            Assert.Equal("ContaStone", transaction.Bank);
            Assert.Equal("Teste", transaction.Description);
            Assert.Equal("0123456789", transaction.Document);
            Assert.Equal(DateTime.MaxValue, transaction.TransactionDate);
            Assert.Equal(DateTime.MinValue, transaction.LaunchDate);
            Assert.Equal(TransactionKind.Entrie, transaction.Kind);
            Assert.Equal(100.5m, transaction.Value);
            Assert.Equal(2.75m, transaction.Charges);

        }


    }
}
