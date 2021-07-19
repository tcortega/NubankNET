using System.Text.Json.Serialization;

namespace tcortega.NubankClient.DTOs
{
    class LoginPayload
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
