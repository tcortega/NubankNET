using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tcortega.NubankClient.DTOs
{
    public class CardFeedLinks
    {
        [JsonPropertyName("updates")]
        public Updates Updates { get; set; }
    }
}
