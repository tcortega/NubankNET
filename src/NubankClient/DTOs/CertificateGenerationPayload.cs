using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    class CertificateGenerationPayload
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }
        [JsonPropertyName("public_key_crypto")]
        public string PublicKeyCrypto { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; }
        [JsonPropertyName("encrypted_code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string EncryptedCode { get; set; }
    }
}
