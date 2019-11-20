using PaymentFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentFlow.Domain.Tests.Entities
{
    public class CashFlowTests
    {

        [Fact]
        public void Properties_Tests()
        {

            CashFlow cashFlow = new CashFlow()
            {
                Date = DateTime.MaxValue,
                PositionOfTheDay = 100, 
                Total = 7080,                
            };

            Assert.Null(cashFlow.Entries);
            Assert.Null(cashFlow.Exits);
            Assert.Null(cashFlow.Charges);
            Assert.Equal(DateTime.MaxValue, cashFlow.Date);
            Assert.Equal(100, cashFlow.PositionOfTheDay);
            Assert.Equal(7080, cashFlow.Total);

            cashFlow.Entries = new List<Entrie>();
            cashFlow.Exits = new List<Exit>();
            cashFlow.Charges = new List<Charge>();

            Assert.NotNull(cashFlow.Entries);
            Assert.NotNull(cashFlow.Exits);
            Assert.NotNull(cashFlow.Charges);

        }

    }
}
