using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tcortega.NubankClient.DTOs
{
    public class CardFeed
    {
        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("as_of")]
        public DateTimeOffset AsOf { get; set; }

        [JsonPropertyName("_links")]
        public CardFeedLinks Links { get; set; }
    }
}
