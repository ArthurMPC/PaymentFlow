using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PaymentFlow.Domain.Enums
{
    public enum Brand : int
    {
        [Description("Master Card")]
        MasterCard = 1,
        [Description("Visa")]
        Visa = 2,
    }
}
