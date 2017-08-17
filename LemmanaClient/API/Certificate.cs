using RestSharp;
using System;
using System.Security.Cryptography.X509Certificates;


namespace LemmanaClient.API
{
    class Certificate
    {
        public static RestClient setCertificate(RestClient client)
        {
            X509Certificate2 certificates = new X509Certificate2();
            certificates.Import("./Certificate/lemmana.crt");
            client.ClientCertificates = new X509CertificateCollection() { certificates };
            Console.Out.WriteLine("Loaded " + client.ClientCertificates.Count + " certificate(s)");
            //Console.Out.WriteLine("Certificate Data : " + client.ClientCertificates.Count + "  " + client.ClientCertificates[0].GetPublicKeyString());
            return client;
        }
    }
}
