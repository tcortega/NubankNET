using System.Text.Json.Serialization;

namespace tcortega.NubankClient.Models
{
    class CertificateGenerationPayload
    {
        public string login { get; set; }
        public string password { get; set; }
        public string public_key { get; set; }
        public string public_key_crypto { get; set; }
        public string model { get; set; }
        public string device_id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string code { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string encrypted_code { get; set; }
    }
}
