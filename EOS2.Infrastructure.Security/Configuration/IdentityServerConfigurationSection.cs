namespace EOS2.Infrastructure.Security.Configuration
{
    using System.Configuration;

    public class IdentityServerConfigurationSection : ConfigurationSection
    {
        protected const string ConnectionStringProperty = "connectionString";
        protected const string SiteNameProperty = "siteName";
        protected const string IssuerUriProperty = "issuerUri";
        protected const string CertThumbprintProperty = "certThumbprint";

        [ConfigurationProperty(ConnectionStringProperty, IsRequired = true)]
        public RequiredValueElement ConnectionString
        {
            get { return (RequiredValueElement)this[ConnectionStringProperty]; }
            set { this[ConnectionStringProperty] = value; }
        }

        [ConfigurationProperty(SiteNameProperty, IsRequired = true)]
        public RequiredValueElement SiteName
        {
            get { return (RequiredValueElement)this[SiteNameProperty]; }
            set { this[SiteNameProperty] = value; }
        }

        [ConfigurationProperty(IssuerUriProperty, IsRequired = true)]
        public RequiredValueElement IssuerUri
        {
            get { return (RequiredValueElement)this[IssuerUriProperty]; }
            set { this[IssuerUriProperty] = value; }
        }

        [ConfigurationProperty(CertThumbprintProperty, IsRequired = true)]
        public RequiredValueElement CertThumbprint
        {
            get { return (RequiredValueElement)this[CertThumbprintProperty]; }
            set { this[CertThumbprintProperty] = value; }
        }
    }
}
