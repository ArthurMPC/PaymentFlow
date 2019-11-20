using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PaymentFlow.Domain.Enums
{
    public enum TransactionKind
    {
        [Description("Exit")]
        Exit = 1,
        [Description("Entrie")]
        Entrie = 2
    }
}
