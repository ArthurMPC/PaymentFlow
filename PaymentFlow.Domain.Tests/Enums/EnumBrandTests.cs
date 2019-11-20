using PaymentFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentFlow.Domain.Tests.Enums
{
    public class EnumBrandTests
    {
        [Fact]
        public void Description_Test()
        {
            Assert.Equal("Master Card", Brand.MasterCard.GetDescription());
            Assert.Equal("Visa", Brand.Visa.GetDescription());

            Assert.NotEqual(Brand.MasterCard.GetDescription(), Brand.Visa.GetDescription());
        }

        [Fact]
        public void Value_Test()
        {
            Assert.Equal(1, Brand.MasterCard.GetValue());
            Assert.Equal(2, Brand.Visa.GetValue());

            Assert.NotEqual(Brand.MasterCard.GetValue(), Brand.Visa.GetValue());
        }

    }
}
