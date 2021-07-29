using System.Text.Json.Serialization;
using tcortega.NubankClient.Enums;
using tcortega.NubankClient.Utilities;

namespace tcortega.NubankClient.DTOs
{
    class Details
    {
        [JsonPropertyName("status")]
        [JsonConverter(typeof(NubankNullableStringEnumConverter<Status?>))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Status? Status { get; set; }

        [JsonPropertyName("subcategory")]
        [JsonConverter(typeof(NubankStringEnumConverter<Subcategory>))]
        public Subcategory Subcategory { get; set; }

        [JsonPropertyName("fx")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Fx Fx { get; set; }

        [JsonPropertyName("charges")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Charges Charges { get; set; }

        [JsonPropertyName("chargeback_requested")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ChargebackRequested { get; set; }

        [JsonPropertyName("lat")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? Lat { get; set; }

        [JsonPropertyName("lon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? Lon { get; set; }
    }
}