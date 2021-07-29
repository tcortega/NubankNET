using System;
using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    class Updates
    {
        [JsonPropertyName("href")]
        public Uri Href { get; set; }
    }
}