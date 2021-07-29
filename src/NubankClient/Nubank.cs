using System;
using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;
using tcortega.NubankClient.Exceptions;
using tcortega.NubankClient.DTOs;
using tcortega.NubankClient.Utilities;
using System.Text.Json;
using System.Collections.Generic;

namespace tcortega.NubankClient
{
    public class Nubank
    {
        private readonly string _cpf;
        private readonly string _password;
        private readonly string _certPath;
        private readonly string _loginUrl;
        private readonly Discovery _discovery;
        private NuHttp _nuHttp;

        private string _feedUrl;
        private string _billsUrl;
        private string _customerUrl;
        private string _queryUrl;
        private string _revokeTokenUrl;


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
            _loginUrl = _discovery.AppEndPoints.Token;
        }

        #region  Api Methods
        public async Task LoginAsync()
        {
            if (!CertificateGenerator.CertificateAlreadyExists(_certPath))
                await GenerateCertificates();

            _nuHttp = new NuHttp(_certPath);

            using var response = await _nuHttp.Client.PostAsJsonAsync(_loginUrl, GetPayload());
            if (!response.IsSuccessStatusCode)
                throw new NuRequestException((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var jsonResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            SaveAuthData(jsonResponse);
        }

        public async Task<CardFeed> GetCardFeed()
        {
            return await _nuHttp.Client.GetFromJsonAsync<CardFeed>(_feedUrl);
        }
        #endregion

        #region Private Auth/Utilities methods
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
            => File.WriteAllBytes(_certPath, bytes);

        private void SaveAuthData(LoginResponse response)
        {
            _nuHttp.AddHeader("Authorization", $"Bearer {response.AccessToken}");
            SaveEndpoints(response);
        }

        private void SaveEndpoints(LoginResponse response)
        {
            var links = response.Links;
            _feedUrl = links.TryGetValue("events", out var link) ? link.Href : links["magnitude"].Href;
            _billsUrl = links["bills_summary"].Href;
            _customerUrl = links["customer"].Href;
            _queryUrl = links["ghostflame"].Href;
            _revokeTokenUrl = links["revoke_token"].Href;
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
        #endregion
    }
}
