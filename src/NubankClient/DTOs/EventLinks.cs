using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    public class EventLinks
    {
        [JsonPropertyName("self")]
        public Updates Self { get; set; }
    }
}