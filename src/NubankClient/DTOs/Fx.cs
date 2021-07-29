using System.Text.Json.Serialization;
using tcortega.NubankClient.Utilities;

namespace tcortega.NubankClient.DTOs
{
    public class Fx
    {
        [JsonPropertyName("currency_origin")]
        public string CurrencyOrigin { get; set; }

        [JsonPropertyName("amount_origin")]
        public long AmountOrigin { get; set; }

        [JsonPropertyName("amount_usd")]
        public long AmountUsd { get; set; }

        [JsonPropertyName("precise_amount_origin")]
        public string PreciseAmountOrigin { get; set; }

        [JsonPropertyName("precise_amount_usd")]
        public string PreciseAmountUsd { get; set; }

        [JsonPropertyName("exchange_rate")]
        public double ExchangeRate { get; set; }
    }
}