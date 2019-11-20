using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PaymentFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Domain.Entities
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonIgnore]
        public string Id;

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "tipo_de_lancamento")]
        public TransactionKind Kind;
        
        [JsonProperty(PropertyName = "descricao")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "conta_destino")]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "banco_destino")]
        public string Bank { get; set; }

        [JsonProperty(PropertyName = "cpf_cnpj_destino")]
        public string Document { get; set; }

        [JsonProperty(PropertyName = "valor_do_lancamento")]
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Value { get; set; }

        [JsonProperty(PropertyName = "encargos")]
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Charges { get; set; }

        [JsonProperty(PropertyName = "data_de_lancamento")]
        public DateTime LaunchDate { get; set; }

        public DateTime TransactionDate { get; set; }

    }
}
