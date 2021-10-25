namespace EOS2.Data.Migrations.Model
{
    using System;
    using System.Data.Entity.Migrations;

    public static class ConfigurationContextBuilder
    {
        public static DbMigrationsConfiguration GetConfiguration(ConfigurationType configurationType)
        {
            switch (configurationType)
            {
                case ConfigurationType.EOS2:
                    return new EOS2DbContext.Configuration();
                case ConfigurationType.Security:
                    return new SecurityContext.Configuration();
                case ConfigurationType.Elmah:
                    return new ELMAHLogging.Configuration();
            }

            throw new ArgumentOutOfRangeException("configurationType");    
        }
    }
}
