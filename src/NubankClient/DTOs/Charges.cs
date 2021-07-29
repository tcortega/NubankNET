using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    class Charges
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }
    }
}