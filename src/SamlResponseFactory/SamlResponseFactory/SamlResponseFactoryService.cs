using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using SAML2;
using SAML2.Config;
using SAML2.Schema.Core;
using SAML2.Utils;
using SamlResponseFactory.DataTypes;

// ReSharper disable ConvertToLambdaExpression
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace SamlResponseFactory
{
    public static class SamlResponseFactoryService
    {
        public static string CreateSamlResponse(SamlResponseFactoryArgs args)
        {
            if (!args.Certificate.HasPrivateKey)
                throw new Exception("Certificate does not not contains a private key");
            
            var section = Saml2SectionFactory.Create(args.Audience);
            var config = Saml2ConfigFactory.Create(section);
            Saml2Config.Init(config);
            
            var assertionDocument = CreateAssertionDocument(args).DocumentElement;
            var cert = args.Certificate;
            
            var assertion20 = new Saml20Assertion(assertionDocument, null, false);
            assertion20.Sign(cert);
            assertion20.CheckValid(new[] { cert.PublicKey.Key });

            var assertionString = assertion20.GetXml().OuterXml;
            var deserializedAssertionDoc = new XmlDocument { PreserveWhitespace = true };
            deserializedAssertionDoc.Load(new StringReader(assertionString));
            var deserializedAssertion = new Saml20Assertion(deserializedAssertionDoc.DocumentElement, null, false);
            deserializedAssertion.CheckValid(new[] { cert.PublicKey.Key });

            var issueInstant = new IssueInstant(deserializedAssertion.Assertion.IssueInstant.Value);
            var result = CompoundResponse(issueInstant, args.Recipient, args.Issuer, assertionString, args.RequestId);
            return result;
        }
        
        public static XmlDocument CreateAssertionDocument(SamlResponseFactoryArgs args)
        {
            var document = new XmlDocument
            {
                PreserveWhitespace = true,
            };
            var assertion = CreateAssertion(args);
            document.Load(new StringReader(Serialization.SerializeToXmlString(assertion)));
            return document;
        }

        public static Func<string> AssertionIdFactory = NewSamlIdByUnderlineAndGuid;
        public static Func<string> ResponseIdFactory = NewSamlIdByUnderlineAndGuid;
        public static Func<SessionIndex> SessionIndexFactory = () => new SessionIndex(NewSamlIdByUnderlineAndGuid());
        public static Func<IssueInstant> IssueInstantFactory = () => new IssueInstant(DateTime.UtcNow);
        public static Func<string> VersionFactory = () => "2.0";

        private static Assertion CreateAssertion(SamlResponseFactoryArgs args)
        {
            var assertionId = AssertionIdFactory();
            var issueInstant = IssueInstantFactory();
            var version = VersionFactory();
            var notOnOrAfter = issueInstant.DateTime.Add(args.TimeToBeExpired);
            var authnInstant = issueInstant.DateTime;
            var sessionIndex = string.IsNullOrEmpty(args.SessionIndex.Value) 
                ? SessionIndexFactory() : args.SessionIndex.Value;
            
            var assertion = new Assertion
            {
                Issuer = new NameId(),
                Id = assertionId,
                IssueInstant = issueInstant.DateTime,
                Version = version,
            };
            assertion.Issuer.Value = args.Issuer.Value;
            assertion.Subject = new Subject();
            var subjectConfirmation = new SubjectConfirmation
            {
                Method = SubjectConfirmation.BearerMethod,
                SubjectConfirmationData = new SubjectConfirmationData
                {
                    NotOnOrAfter = notOnOrAfter,
                    Recipient = args.Recipient.Value,
                    InResponseTo = args.RequestId.Value,
                },
            };
            assertion.Subject.Items = new object[]
            {
                new NameId
                {
                    Format = "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress",
                    Value = args.Email.Value
                }, 
                subjectConfirmation
            };
            assertion.Conditions = new Conditions { NotOnOrAfter = notOnOrAfter };
            var audienceRestriction = new AudienceRestriction
            {
                Audience = new List<string>(new[] { args.Audience.Value })
            };
            assertion.Conditions.Items = new List<ConditionAbstract>(new ConditionAbstract[]
            {
                audienceRestriction
            });
            AuthnStatement authnStatement;
            {
                authnStatement = new AuthnStatement();
                assertion.Items = new StatementAbstract[] { authnStatement };
                authnStatement.AuthnInstant = authnInstant;
                authnStatement.SessionIndex = sessionIndex;
                authnStatement.AuthnContext = new AuthnContext
                {
                    Items = new object[]
                    {
                        "urn:oasis:names:tc:SAML:2.0:ac:classes:X509"
                    },
                    ItemsElementName = new[]
                    {
                        AuthnContextType.AuthnContextClassRef
                    }
                };
            }
            AttributeStatement attributeStatement;
            {
                attributeStatement = new AttributeStatement();
                var email = new SamlAttribute
                    {
                        Name = "User.email",
                        NameFormat = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic",
                        AttributeValue = new[] { args.Email.Value }
                    };
                attributeStatement.Items = new object[] { email };
            }
            assertion.Items = new StatementAbstract[] { authnStatement, attributeStatement };
            return assertion;
        }
        
        public static string CompoundResponse(IssueInstant issueInstant, Recipient recipient, Issuer issuer, 
            string assertion, RequestId requestId)
        {
            var responseId = ResponseIdFactory();
            var response = 
                $"<samlp:Response xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\" xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"{responseId}\" Version=\"2.0\" IssueInstant=\"{issueInstant.String}\" Destination=\"{recipient.Value}\" InResponseTo=\"{requestId.Value}\">{Environment.NewLine}" +
                $"<saml:Issuer>{issuer.Value}</saml:Issuer>" +
                $"<samlp:Status>" +
                $"<samlp:StatusCode Value=\"urn:oasis:names:tc:SAML:2.0:status:Success\"/>" +
                $"</samlp:Status>" +
                assertion +
                $"</samlp:Response>";
            return response;
        }
        
        public static string NewSamlIdByUnderlineAndGuid()
        {
            return $"_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        }
    }
}