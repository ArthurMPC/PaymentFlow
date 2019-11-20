using PaymentFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentFlow.Domain.Tests.Enums
{
    public class EnumTransactionKindEnumTests
    {
        [Fact]
        public void Description_Test()
        {
            Assert.Equal("Entrie", TransactionKind.Entrie.GetDescription());
            Assert.Equal("Exit", TransactionKind.Exit.GetDescription());

            Assert.NotEqual(TransactionKind.Entrie.GetDescription(), TransactionKind.Exit.GetDescription());
        }

        [Fact]
        public void Value_Test()
        {
            Assert.Equal(2, TransactionKind.Entrie.GetValue());
            Assert.Equal(1, TransactionKind.Exit.GetValue());

            Assert.NotEqual(TransactionKind.Entrie.GetValue(), TransactionKind.Exit.GetValue());
        }

    }
}
