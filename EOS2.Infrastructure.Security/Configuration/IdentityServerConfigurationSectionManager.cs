namespace EOS2.Infrastructure.Security.Configuration
{
    public static class IdentityServerConfigurationSectionManager
    {
        public static IdentityServerConfigurationSection Configuration
        {
            get
            {
                return (IdentityServerConfigurationSection)System
                                                     .Configuration
                                                     .ConfigurationManager
                                                     .GetSection("EOS.IdentityServerConfiguration");
            }
        }
    }
}
