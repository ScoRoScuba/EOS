namespace EOS2.Web
{
    public static class LoggingConfig
    {
        public static void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();            
        }
    }
}