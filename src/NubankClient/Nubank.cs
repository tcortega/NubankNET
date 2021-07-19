using System;
using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;
using tcortega.NubankClient.Exceptions;
using tcortega.NubankClient.DTOs;
using tcortega.NubankClient.Utilities;
using System.Text.Json;

namespace tcortega.NubankClient
{
    public class Nubank
    {
        private string _cpf;
        private string _password;
        private string _certPath;
        private string _baseUrl;
        private NuHttp _nuHttp;
        private Discovery _discovery;

        public Nubank(string cpf, string password, string certPath)
            : this(cpf, password, certPath, new NuHttp())
        {

        }

        private Nubank(string cpf, string password, string certPath, NuHttp nuHttp)
        {
            _cpf = cpf;
            _password = password;
            _certPath = certPath;
            _nuHttp = nuHttp;
            _discovery = new Discovery(nuHttp);
            _baseUrl = _discovery.AppEndPoints.Token;
        }

        public async Task LoginAsync()
        {
            if (!CertificateGenerator.CertificateAlreadyExists(_certPath))
                await GenerateCertificates();

            _nuHttp = new NuHttp(_certPath);

            using var response = await _nuHttp.Client.PostAsJsonAsync(_baseUrl, GetPayload());
            if (!response.IsSuccessStatusCode)
                throw new NuRequestException((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var jsonResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            SaveAuthData(jsonResponse);
        }

        private async Task GenerateCertificates()
        {
            var certGenerator = new CertificateGenerator(_cpf, _password, _certPath, _nuHttp, _discovery);
            string mail;

            Console.WriteLine("[*] Requesting 2 Factor Authentication Code");
            try
            {
                mail = await certGenerator.Request2FA();
            }
            catch (Exception)
            {
                throw new NuException("Failed to request verification code. Check your credentials!");
            }

            Console.WriteLine($"[*] Email sent to {mail}");
            Console.Write($"[>] Type the received code: ");
            var code = Console.ReadLine();

            var pkcs12Cert = await certGenerator.ExchangeCerts(code);
            SaveCert(pkcs12Cert);
        }

        private void SaveCert(byte[] bytes)
        {
            File.WriteAllBytes(_certPath, bytes);
        }

        private void SaveAuthData(LoginResponse response)
        {
            _nuHttp.Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {response.AccessToken}");

        }

        private LoginPayload GetPayload()
        {
            return new LoginPayload()
            {
                GrantType = "password",
                ClientId = "legacy_client_id",
                ClientSecret = "legacy_client_secret",
                Login = _cpf,
                Password = _password
            };
        }
    }
}
