using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamlResponseFactory.DataTypes;
// ReSharper disable StringLiteralTypo

namespace SamlResponseFactory.Tests
{
    [TestClass]
    public sealed class UsageExample
    {
        [TestMethod]
        public void CreateSamlResponseTest()
        {
            var samlResponse = SamlResponseFactoryService.CreateSamlResponse(new SamlResponseFactoryArgs
            {
                Audience = new Audience("http://sp.example.com/demo1/metadata.php"),
                Issuer = new Issuer("http://idp.example.com/metadata.php"),
                Recipient = new Recipient("http://sp.example.com/demo1/index.php?acs"),
                Certificate = new X509Certificate2("zohoSso.pfx"),
                TimeToBeExpired = TimeSpan.FromMinutes(30),
                RequestId = new RequestId("_c635b3bdaa8f4b529368b6dabe01d5d91539326"),
                Email = new Email("userEmail@gmail.com"),
            });
            Console.WriteLine(samlResponse);
        }
    }
}