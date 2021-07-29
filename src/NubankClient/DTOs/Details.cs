using System.Text.Json.Serialization;
using tcortega.NubankClient.Utilities;

namespace tcortega.NubankClient.DTOs
{
    public class Details
    {
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Status { get; set; }

        [JsonPropertyName("subcategory")]
        public string Subcategory { get; set; }

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