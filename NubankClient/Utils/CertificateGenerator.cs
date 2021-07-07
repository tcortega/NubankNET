using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace tcortega.NubankClient.Utils
{
    class CertificateGenerator
    {
        private string _cpf;
        private string _password;
        private string _deviceId;
        private RSAOpenSsl _rsaKey1;
        private RSAOpenSsl _rsaKey2;
        private Discovery _discovery;
        private string _baseUrl;

        public CertificateGenerator(string cpf, string password, string deviceId)
        {
            _cpf = cpf;
            _password = password;
            _deviceId = deviceId;
            _rsaKey1 = new RSAOpenSsl(2048); 
            _rsaKey2 = new RSAOpenSsl(2048);
            _discovery = new Discovery();
            _baseUrl = _discovery.GetAppUrl("gen_certificate");
        }
    }
}
