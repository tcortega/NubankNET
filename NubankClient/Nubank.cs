using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tcortega.NubankClient.Exceptions;
using tcortega.NubankClient.Helpers;
using tcortega.NubankClient.Utils;

namespace tcortega.NubankClient
{
    public class Nubank
    {
        private string _cpf;
        private string _password;
        private string _certPath;
        private NuHttp _nuHttp;

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
        }

        public async Task LoginAsync()
        {
            if (!CertificateGenerator.CertificateAlreadyExists(_certPath))
            {
                await GenerateCertificates();
            }

        }

        private async Task GenerateCertificates()
        {
            try
            {
                var certGenerator = new CertificateGenerator(_cpf, _password, _certPath, _nuHttp);
                Console.WriteLine("Requesting 2 Factor Authentication Code");
                await certGenerator.Request2FA();
            }
            catch (Exception)
            {
                throw new NuException("Failed to request code. Check your credentials!");
            }
        }
        //public static Nubank Create(string cpf, string password, string certPath)
        //{
        //    return CertificateGenerator.CertificateAlreadyExists(certPath)
        //        ? new Nubank(cpf, password, certPath)
        //        : new Nubank(cpf, password, certPath, new NuHttp());
        //}

        //private Nubank(string cpf, string password, string certPath)
        //{

        //}

        //private Nubank(string cpf, string password, string certPath, NuHttp nuHttp)
        //{
        //    _nuHttp = nuHttp;
        //    var certGenerator = new CertificateGenerator(cpf, password, certPath, nuHttp);
        //}
    }
}
