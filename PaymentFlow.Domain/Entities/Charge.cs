using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Domain.Entities
{
    public class Charge
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "valor")]
        public decimal Value { get; set; }
    }
}
