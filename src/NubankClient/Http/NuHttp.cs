using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace tcortega.NubankClient.Utilities
{
    class NuHttp
    {
        public readonly HttpClient Client;
        public NuHttp()
        {
            Client = new HttpClient();
            SetDefaultHeaders();
        }

        public NuHttp(string certPath)
        {
            var cert = File.ReadAllBytes(certPath);
            var handler = CreateHandlerWithCertificate(cert);
            Client = new HttpClient(handler);
            SetDefaultHeaders();
        }

        private void SetDefaultHeaders()
        {
            AddHeader("Content-Type", "application/json");
            AddHeader("X-Correlation-Id", "WEB-APP.pewW9");
            AddHeader("User-Agent", "NubankNET Client");
        }

        private static HttpClientHandler CreateHandlerWithCertificate(byte[] certBytes)
        {
            var cert = new X509Certificate2(certBytes);
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);

            return handler;
        }

        public void AddHeader(string name, string value)
            => Client.DefaultRequestHeaders.TryAddWithoutValidation(name, value);

        public void RemoveHeader(string name) 
            => Client.DefaultRequestHeaders.Remove(name);
    }
}
