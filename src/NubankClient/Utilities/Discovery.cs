using System.Net.Http.Json;
using tcortega.NubankClient.DTOs;

namespace tcortega.NubankClient.Utilities
{
    class Discovery
    {
        public readonly AppEndpoints AppEndPoints;
        private static readonly string DISCOVERY_APP_URL = "https://prod-s0-webapp-proxy.nubank.com.br/api/app/discovery";

        public Discovery(NuHttp nuHttp)
        {
            // TO-DO: Remove this blocking call from the ctor
            AppEndPoints = nuHttp.Client.GetFromJsonAsync<AppEndpoints>(DISCOVERY_APP_URL).GetAwaiter().GetResult();
        }
    }
}
