using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using tcortega.NubankClient.Exceptions;

namespace tcortega.NubankClient.Utilities
{
    class NuHttp
    {
        public readonly HttpClient Client;

        //private Dictionary<string, string> _defaultHeaders = new Dictionary<string, string>()
        //    {
        //        { "Content-Type", "application/json" },
        //        { "X-Correlation-Id", "WEB-APP.pewW9" },
        //        { "User-Agent", "NubankNET Client - https://github.com/tcortega/NubankNET" }
        //    };

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
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            Client.DefaultRequestHeaders.TryAddWithoutValidation("X-Correlation-Id", "WEB-APP.pewW9");
            Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "NubankNET Client");
        }

        private static HttpClientHandler CreateHandlerWithCertificate(byte[] certBytes)
        {
            var cert = new X509Certificate2(certBytes);
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);

            return handler;
        }

        public void AddHeader(string name, string value)
        {
            Client.DefaultRequestHeaders.Add(name, value);
        }

        public void AddHeader(Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                AddHeader(header.Key, header.Value);
            }
        }

        public void RemoveHeader(string name)
        {
            Client.DefaultRequestHeaders.Remove(name);
        }
    }
}
