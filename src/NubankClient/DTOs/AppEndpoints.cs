using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    class AppEndpoints
    {
        [JsonPropertyName("gen_certificate")]
        public string GenCertificate { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
