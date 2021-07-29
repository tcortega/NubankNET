using System;
using System.Text.Json.Serialization;
using tcortega.NubankClient.Utilities;

namespace tcortega.NubankClient.DTOs
{
    public class Event
    {
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("amount")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? Amount { get; set; }

        [JsonPropertyName("time")]
        public DateTimeOffset Time { get; set; }

        [JsonPropertyName("source")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Source { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("amount_without_iof")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? AmountWithoutIof { get; set; }

        [JsonPropertyName("account")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? Account { get; set; }

        [JsonPropertyName("details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Details Details { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EventLinks Links { get; set; }

        [JsonPropertyName("tokenized")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Tokenized { get; set; }

        [JsonPropertyName("href")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Href { get; set; }
    }
}