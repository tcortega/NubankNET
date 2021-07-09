using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using tcortega.NubankClient.Helpers;
using tcortega.NubankClient.Models;
using tcortega.NubankClient.Utilities;

namespace tcortega.NubankClient.Utils
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

        public CertificateGenerator(string cpf, string password, string certPath, NuHttp nuHttp)
        {
            _cpf = cpf;
            _password = password;
            _rsaKey1 = new RSACryptoServiceProvider(2048);
            _rsaKey2 = new RSACryptoServiceProvider(2048);
            _discovery = new Discovery(nuHttp);
            _nuHttp = nuHttp;
            _baseUrl = _discovery.AppEndPoints.gen_certificate;
        }

        public static bool CertificateAlreadyExists(string path)
        {
            return File.Exists(path);
        }

        public async Task Request2FA()
        {
            var response = await _nuHttp.Client.PostAsync(_baseUrl, GetPayload());
        }

        private JsonContent<LoginPayload> GetPayload()
        {
            var loginModel = new LoginPayload()
            {
                login = _cpf,
                password = _password,
                device_id = _deviceId,
                model = $"NubankNET Client ({_deviceId})",
                public_key = RSAKeys.ExportPublicKey(_rsaKey1),
                public_key_crypto = RSAKeys.ExportPublicKey(_rsaKey2)
            };

            return new JsonContent<LoginPayload>(loginModel);
        }
    }
}
