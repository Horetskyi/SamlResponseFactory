using SAML2.Config;
using SAML2.Config.ConfigurationManager;

namespace SamlResponseFactory
{
    /// <summary>
    /// Just needed for SAML2 library to get to work
    /// </summary>
    public static class Saml2ConfigFactory
    {
        /// <summary>
        /// I took code from original Saml2Section.GetConfig() and changed it to get it to work.
        /// </summary>
        public static Saml2Config Create(Saml2Section configElement)
        {
            Saml2Config saml2Config = new Saml2Config();
            if (configElement.Actions.ElementInformation.IsPresent)
            {
                foreach (ActionElement action in configElement
                    .Actions)
                    saml2Config.Actions.Add(new Action
                    {
                        Name = action.Name,
                        Type = action.Type
                    });
            }

            if (true || configElement.AllowedAudienceUris.ElementInformation.IsPresent)
            {
                foreach (AudienceUriElement allowedAudienceUri in configElement.AllowedAudienceUris)
                    saml2Config.AllowedAudienceUris.Add(allowedAudienceUri.Uri);
            }

            if (configElement.AssertionProfile.ElementInformation.IsPresent)
                saml2Config.AssertionProfile.AssertionValidator = configElement.AssertionProfile.AssertionValidator;
            if (configElement.CommonDomainCookie.ElementInformation.IsPresent)
            {
                saml2Config.CommonDomainCookie.Enabled = configElement.CommonDomainCookie.Enabled;
                saml2Config.CommonDomainCookie.LocalReaderEndpoint =
                    configElement.CommonDomainCookie.LocalReaderEndpoint;
            }

            if (true || configElement.IdentityProviders.ElementInformation.IsPresent)
            {
                saml2Config.IdentityProviderSelectionUrl = configElement.IdentityProviders.SelectionUrl;
                saml2Config.IdentityProviders.Encodings = configElement.IdentityProviders.Encodings;
                saml2Config.IdentityProviders.MetadataLocation = configElement.IdentityProviders.MetadataLocation;
                foreach (IdentityProviderElement identityProvider1 in configElement.IdentityProviders)
                {
                    IdentityProvider identityProvider2 = new IdentityProvider
                    {
                        AllowUnsolicitedResponses = identityProvider1.AllowUnsolicitedResponses,
                        Default = identityProvider1.Default,
                        ForceAuth = identityProvider1.ForceAuth,
                        Id = identityProvider1.Id,
                        IsPassive = identityProvider1.IsPassive,
                        Name = identityProvider1.Name,
                        OmitAssertionSignatureCheck = identityProvider1.OmitAssertionSignatureCheck,
                        QuirksMode = identityProvider1.QuirksMode,
                        ResponseEncoding = identityProvider1.ResponseEncoding
                    };
                    if (identityProvider1.ArtifactResolution.ElementInformation.IsPresent)
                    {
                        HttpAuth httpAuth = new HttpAuth();
                        if (identityProvider1.ArtifactResolution.ClientCertificate.ElementInformation.IsPresent)
                            httpAuth.ClientCertificate = new Certificate
                            {
                                FindValue = identityProvider1.ArtifactResolution.ClientCertificate.FindValue,
                                StoreLocation = identityProvider1.ArtifactResolution.ClientCertificate.StoreLocation,
                                StoreName = identityProvider1.ArtifactResolution.ClientCertificate.StoreName,
                                ValidOnly = identityProvider1.ArtifactResolution.ClientCertificate.ValidOnly,
                                X509FindType = identityProvider1.ArtifactResolution.ClientCertificate.X509FindType
                            };
                        if (identityProvider1.ArtifactResolution.Credentials.ElementInformation.IsPresent)
                            httpAuth.Credentials = new HttpAuthCredentials
                            {
                                Password = identityProvider1.ArtifactResolution.Credentials.Password,
                                Username = identityProvider1.ArtifactResolution.Credentials.Username
                            };
                        identityProvider2.ArtifactResolution = httpAuth;
                    }

                    if (identityProvider1.AttributeQuery.ElementInformation.IsPresent)
                    {
                        HttpAuth httpAuth = new HttpAuth();
                        if (identityProvider1.AttributeQuery.ClientCertificate.ElementInformation.IsPresent)
                            httpAuth.ClientCertificate = new Certificate
                            {
                                FindValue = identityProvider1.AttributeQuery.ClientCertificate.FindValue,
                                StoreLocation = identityProvider1.AttributeQuery.ClientCertificate.StoreLocation,
                                StoreName = identityProvider1.AttributeQuery.ClientCertificate.StoreName,
                                ValidOnly = identityProvider1.AttributeQuery.ClientCertificate.ValidOnly,
                                X509FindType = identityProvider1.AttributeQuery.ClientCertificate.X509FindType
                            };
                        if (identityProvider1.AttributeQuery.Credentials.ElementInformation.IsPresent)
                            httpAuth.Credentials = new HttpAuthCredentials
                            {
                                Password = identityProvider1.AttributeQuery.Credentials.Password,
                                Username = identityProvider1.AttributeQuery.Credentials.Username
                            };
                        identityProvider2.AttributeQuery = httpAuth;
                    }

                    if (identityProvider1.PersistentPseudonym.ElementInformation.IsPresent)
                        identityProvider2.PersistentPseudonym = new PersistentPseudonym
                        {
                            Mapper = identityProvider1.PersistentPseudonym.Mapper
                        };
                    foreach (CertificateValidationElement certificateValidation in identityProvider1
                        .CertificateValidations)
                        identityProvider2.CertificateValidations.Add(certificateValidation.Type);
                    foreach (string allKey in identityProvider1.CommonDomainCookie.AllKeys)
                        identityProvider2.CommonDomainCookie.Add(identityProvider1.CommonDomainCookie[allKey].Key,
                            identityProvider1.CommonDomainCookie[allKey].Value);
                    foreach (IdentityProviderEndpointElement endpoint in identityProvider1
                        .Endpoints)
                        identityProvider2.Endpoints.Add(new IdentityProviderEndpoint
                        {
                            Binding = endpoint.Binding,
                            ForceProtocolBinding = endpoint.ForceProtocolBinding,
                            TokenAccessor = endpoint.TokenAccessor,
                            Type = endpoint.Type,
                            Url = endpoint.Url
                        });
                    saml2Config.IdentityProviders.Add(identityProvider2);
                }
            }

            if (configElement.Logging.ElementInformation.IsPresent)
                saml2Config.Logging.LoggingFactory = configElement.Logging.LoggingFactory;
            if (true || configElement.Metadata.ElementInformation.IsPresent)
            {
                saml2Config.Metadata.ExcludeArtifactEndpoints = configElement.Metadata.ExcludeArtifactEndpoints;
                saml2Config.Metadata.Lifetime = configElement.Metadata.Lifetime;
                if (configElement.Metadata.Organization.ElementInformation.IsPresent)
                    saml2Config.Metadata.Organization = new Organization
                    {
                        Name = configElement.Metadata.Organization.Name,
                        DisplayName = configElement.Metadata.Organization.DisplayName,
                        Url = configElement.Metadata.Organization.Url
                    };
                foreach (ContactElement contact in configElement.Metadata.Contacts)
                    saml2Config.Metadata.Contacts.Add(new Contact
                    {
                        Company = contact.Company,
                        Email = contact.Email,
                        GivenName = contact.GivenName,
                        Phone = contact.Phone,
                        SurName = contact.SurName,
                        Type = contact.Type
                    });
                foreach (AttributeElement requestedAttribute in configElement.Metadata
                    .RequestedAttributes)
                    saml2Config.Metadata.RequestedAttributes.Add(new Attribute
                    {
                        IsRequired = requestedAttribute.IsRequired,
                        Name = requestedAttribute.Name
                    });
            }

            if (true || configElement.ServiceProvider.ElementInformation.IsPresent)
            {
                saml2Config.ServiceProvider.AuthenticationContextComparison =
                    configElement.ServiceProvider.AuthenticationContexts.Comparison;
                saml2Config.ServiceProvider.Id = configElement.ServiceProvider.Id;
                saml2Config.ServiceProvider.NameIdFormatAllowCreate =
                    configElement.ServiceProvider.NameIdFormats.AllowCreate;
                saml2Config.ServiceProvider.Server = configElement.ServiceProvider.Server;
                if (configElement.ServiceProvider.SigningCertificate.ElementInformation.IsPresent)
                    saml2Config.ServiceProvider.SigningCertificate = new Certificate
                    {
                        FindValue = configElement.ServiceProvider.SigningCertificate.FindValue,
                        StoreLocation = configElement.ServiceProvider.SigningCertificate.StoreLocation,
                        StoreName = configElement.ServiceProvider.SigningCertificate.StoreName,
                        ValidOnly = configElement.ServiceProvider.SigningCertificate.ValidOnly,
                        X509FindType = configElement.ServiceProvider.SigningCertificate.X509FindType
                    };
                foreach (AuthenticationContextElement authenticationContext in configElement
                    .ServiceProvider.AuthenticationContexts)
                    saml2Config.ServiceProvider.AuthenticationContexts.Add(new AuthenticationContext
                    {
                        Context = authenticationContext.Context,
                        ReferenceType = authenticationContext.ReferenceType
                    });
                foreach (ServiceProviderEndpointElement endpoint in configElement
                    .ServiceProvider.Endpoints)
                    saml2Config.ServiceProvider.Endpoints.Add(new ServiceProviderEndpoint
                    {
                        Binding = endpoint.Binding,
                        Index = endpoint.Index,
                        LocalPath = endpoint.LocalPath,
                        RedirectUrl = endpoint.RedirectUrl,
                        Type = endpoint.Type
                    });
                foreach (NameIdFormatElement nameIdFormat in configElement.ServiceProvider
                    .NameIdFormats)
                    saml2Config.ServiceProvider.NameIdFormats.Add(nameIdFormat.Format);
            }

            if (configElement.State.ElementInformation.IsPresent)
            {
                saml2Config.State.StateServiceFactory = configElement.State.StateServiceFactory;
                foreach (StateSettingElement setting in configElement.State.Settings)
                    saml2Config.State.Settings.Add(setting.Name, setting.Value);
            }

            return saml2Config;
        }
    }
}