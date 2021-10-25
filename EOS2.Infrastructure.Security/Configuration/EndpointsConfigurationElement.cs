namespace EOS2.Infrastructure.Security.Configuration
{
    using System.Configuration;

    public class EndpointsConfigurationSection : ConfigurationElement
    {
        protected const string AuthorizeEndpointProperty = "authorizeEndpoint";
        protected const string TokenEndpointProperty = "tokenEndpoint";
        protected const string UserInfoEndpointProperty = "userInfoEndpoint";
        protected const string EndSessionEndpointProperty = "endsessionEndpoint";

        [ConfigurationProperty(AuthorizeEndpointProperty, IsRequired = false)]
        public ValueElement AuthorizeEndpoint 
        {
            get { return (ValueElement)this[AuthorizeEndpointProperty]; }            
            set { this[AuthorizeEndpointProperty] = value; }
        }

        [ConfigurationProperty(TokenEndpointProperty, IsRequired = false)]
        public ValueElement TokenEndpoint
        {
            get { return (ValueElement)this[TokenEndpointProperty]; }            
            set { this[TokenEndpointProperty] = value; }
        }

        [ConfigurationProperty(UserInfoEndpointProperty, IsRequired = false)]
        public ValueElement UserInfoEndpoint
        {
            get { return (ValueElement)this[UserInfoEndpointProperty]; }            
            set { this[UserInfoEndpointProperty] = value; }
        }

        [ConfigurationProperty(EndSessionEndpointProperty, IsRequired = false)]
        public ValueElement EndSessionEndpoint 
        {
            get { return (ValueElement)this[EndSessionEndpointProperty]; }            
            set { this[EndSessionEndpointProperty] = value; }
        }
    }
}
