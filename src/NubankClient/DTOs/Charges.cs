using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    public class Charges
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }
    }
}