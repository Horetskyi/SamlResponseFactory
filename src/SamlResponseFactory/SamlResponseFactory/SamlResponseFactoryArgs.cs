using System;
using System.Security.Cryptography.X509Certificates;
using SamlResponseFactory.DataTypes;

namespace SamlResponseFactory
{
    public sealed class SamlResponseFactoryArgs
    {
        public Issuer Issuer { get; set; }
        public Recipient Recipient { get; set; }
        public Audience Audience { get; set; }
        public RequestId RequestId { get; set; }
        public Email Email { get; set; }
        public TimeSpan TimeToBeExpired { get; set; }
        public SessionIndex SessionIndex { get; set; }
        public X509Certificate2 Certificate { get; set; }
    }
}