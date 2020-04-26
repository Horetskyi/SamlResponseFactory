using SAML2.Config;
using SAML2.Config.ConfigurationManager;
using SamlResponseFactory.DataTypes;

namespace SamlResponseFactory
{
    /// <summary>
    /// Just needed for SAML2 library to get to work
    /// </summary>
    public static class Saml2SectionFactory
    {
        /// <summary>
        /// Just needed for SAML2 library to get to work
        /// </summary>
        public static Saml2Section Create(Audience audience)
        {
            var section = new Saml2Section
            {
                ServiceProvider = 
                {
                    Id = "https://workaround.com", 
                    Server = "http://",
                },
                AllowedAudienceUris = 
                {
                    new AudienceUriElement
                    {
                        Uri = audience.Value
                    },
                },
            };
            return section;
        }
    }
}