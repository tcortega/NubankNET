using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using tcortega.NubankClient.Exceptions;
using tcortega.NubankClient.Helpers;
using tcortega.NubankClient.DTOs;

namespace tcortega.NubankClient.Utilities
{
    class CertificateGenerator
    {
        private string _cpf;
        private string _password;
        private string _deviceId;
        private RSACryptoServiceProvider _rsaKey1;
        private RSACryptoServiceProvider _rsaKey2;
        private Discovery _discovery;
        private string _baseUrl;
        private NuHttp _nuHttp;
        private string _encryptedCode;

        public CertificateGenerator(string cpf, string password, string certPath, NuHttp nuHttp, Discovery discovery)
        {
            _cpf = cpf;
            _password = password;
            _deviceId = Generators.RandomString(12);
            _rsaKey1 = new RSACryptoServiceProvider(2048);
            _rsaKey2 = new RSACryptoServiceProvider(2048);
            _discovery = discovery;
            _nuHttp = nuHttp;
            _baseUrl = _discovery.AppEndPoints.GenCertificate;
        }

        public static bool CertificateAlreadyExists(string path)
        {
            return File.Exists(path);
        }

        public async Task<string> Request2FA()
        {
            using var response = await _nuHttp.Client.PostAsJsonAsync(_baseUrl, GetPayload());

            if ((int)response.StatusCode != 401 || !response.Headers.Contains("WWW-Authenticate"))
                throw new NuException("Authentication code request failed.");

            var authHeader = response.Headers.WwwAuthenticate.ToString();
            var parsedArgs = ParseArgsFromAuthHeader(authHeader);

            _encryptedCode = parsedArgs["encryptedCode"];

            return parsedArgs["mail"];
        }

        public async Task<byte[]> ExchangeCerts(string code)
        {
            if (_encryptedCode is null)
                throw new NuException("No encrypted code was found. There was an error in the certificate generation.");

            var payload = GetPayload(code);
            using var response = await _nuHttp.Client.PostAsJsonAsync(_baseUrl, payload);

            if (!response.IsSuccessStatusCode)
                throw new NuException(await response.Content.ReadAsStringAsync());

            var content = await response.Content.ReadFromJsonAsync<CertificateGeneration>();
            var certBytes = Encoding.ASCII.GetBytes(content.Certificate);
            var certificate = new X509Certificate2(certBytes);

            return GetPKCS12Cert(certificate);
        }

        private CertificateGenerationPayload GetPayload(string code = null)
        {
            return new CertificateGenerationPayload()
            {
                Login = _cpf,
                Password = _password,
                DeviceId = _deviceId,
                Model = $"NubankNET Client ({_deviceId})",
                PublicKey = RSAKeys.ExportPublicKey(_rsaKey1),
                PublicKeyCrypto = RSAKeys.ExportPublicKey(_rsaKey2),
                Code = code,
                EncryptedCode = _encryptedCode
            };
        }

        private static Dictionary<string, string> ParseArgsFromAuthHeader(string header)
        {
            var encryptedCode = Parsers.ParseBetween(header, "encrypted-code=\"", "\"").FirstOrDefault();
            var mail = Parsers.ParseBetween(header, "sent-to=\"", "\"").FirstOrDefault();

            return new Dictionary<string, string>()
            {
                { "encryptedCode", encryptedCode },
                { "mail", mail }
            };
        }

        private byte[] GetPKCS12Cert(X509Certificate2 cert)
        {
            var newCert = cert.CopyWithPrivateKey(_rsaKey1);
            return newCert.Export(X509ContentType.Pkcs12);
        }
    }
}
