using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Domain.Entities
{
    public class CashFlow
    {
        [JsonProperty(PropertyName = "data")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "entradas")]
        public List<Entrie> Entries { get; set; }

        [JsonProperty(PropertyName = "saidas")]
        public List<Exit> Exits { get; set; }

        [JsonProperty(PropertyName = "encargos")]
        public List<Charge> Charges { get; set; }

        [JsonProperty(PropertyName = "total")]
        public decimal Total { get; set; }

        [JsonProperty(PropertyName = "posicao_do_dia")]
        public decimal PositionOfTheDay { get; set; }
    }
}
