namespace EOS2.Infrastructure.Security.Configuration
{
    using System.Configuration;

    public class IdentityConfigurationSection : ConfigurationSection
    {
        protected const string EnabledProperty = "enabled";
        protected const string AudienceProperty = "audience";
        protected const string ClientIdProperty = "clientId";
        protected const string IdentityServerUriProperty = "identityServerUri";
        protected const string RedirectUriProperty = "redirectUri";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout", Justification = "Named as expected")]
        protected const string LogoutRedirectUriProperty = "logoutRedirectUri";
        protected const string ScopesProperty = "scopes";
        protected const string SecretProperty = "secret";
        protected const string CertThumbprintProperty = "certThumbprint";
        protected const string ValidIssuerProperty = "validIssuer";

        protected const string EndpointsProperty = "Endpoints";

        [ConfigurationProperty(EnabledProperty, DefaultValue = "true", IsRequired = false)]
        public bool Enabled
        {
            get { return (bool)this[EnabledProperty]; }            
            set { this[EnabledProperty] = value; }
        }

        [ConfigurationProperty(AudienceProperty, IsRequired = true)]
        public RequiredValueElement Audience 
        {
            get { return (RequiredValueElement)this[AudienceProperty]; }
            set { this[AudienceProperty] = value; }
        }

        [ConfigurationProperty(ClientIdProperty, IsRequired = true)]
        public RequiredValueElement ClientId
        {
            get { return (RequiredValueElement)this[ClientIdProperty]; }
            set { this[ClientIdProperty] = value; }
        }

        [ConfigurationProperty(IdentityServerUriProperty, IsRequired = true)]
        public RequiredValueElement IdentityServerUri
        {
            get { return (RequiredValueElement)this[IdentityServerUriProperty]; }
            set { this[IdentityServerUriProperty] = value; }
        }

        [ConfigurationProperty(RedirectUriProperty, IsRequired = true)]
        public RequiredValueElement RedirectUri
        {
            get { return (RequiredValueElement)this[RedirectUriProperty]; }
            set { this[RedirectUriProperty] = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout", Justification = "Named as expected")]
        [ConfigurationProperty(LogoutRedirectUriProperty, IsRequired = true)]
        public RequiredValueElement LogoutRedirectUri
        {
            get { return (RequiredValueElement)this[LogoutRedirectUriProperty]; }
            set { this[LogoutRedirectUriProperty] = value; }
        }

        [ConfigurationProperty(ScopesProperty, IsRequired = true)]
        public RequiredValueElement Scopes
        {
            get { return (RequiredValueElement)this[ScopesProperty]; }
            set { this[ScopesProperty] = value; }
        }

        [ConfigurationProperty(SecretProperty, IsRequired = true)]
        public RequiredValueElement Secret
        {
            get { return (RequiredValueElement)this[SecretProperty]; }
            set { this[SecretProperty] = value; }
        }

        [ConfigurationProperty(CertThumbprintProperty, IsRequired = true)]
        public RequiredValueElement CertThumbprint
        {
            get { return (RequiredValueElement)this[CertThumbprintProperty]; }
            set { this[CertThumbprintProperty] = value; }
        }

        [ConfigurationProperty(ValidIssuerProperty, IsRequired = true)]
        public RequiredValueElement ValidIssuer
        {
            get { return (RequiredValueElement)this[ValidIssuerProperty]; }
            set { this[ValidIssuerProperty] = value; }
        }

        [ConfigurationProperty(EndpointsProperty)]
        public EndpointsConfigurationSection Endpoints
        {
            get { return (EndpointsConfigurationSection)this[EndpointsProperty]; }
            set { this[EndpointsProperty] = value; }
        }
    }
}
