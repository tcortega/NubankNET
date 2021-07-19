using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    class CertificateGeneration
    {
        [JsonPropertyName("certificate")]
        public string Certificate { get; set; }
        [JsonPropertyName("certificate_crypto")]
        public string CertificateCrypto { get; set; }
    }
}
