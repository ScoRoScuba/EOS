namespace EOS2.Infrastructure.Security.Configuration
{
    public static class IdentityServerConfigurationManager
    {
        public static IdentityConfigurationSection Configuration
        {
            get
            {
                return (IdentityConfigurationSection)System
                                                     .Configuration
                                                     .ConfigurationManager
                                                     .GetSection("EOS.IdentityServer");
            }
        }
    }
}
