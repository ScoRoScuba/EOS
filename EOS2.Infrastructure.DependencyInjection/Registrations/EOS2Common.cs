namespace EOS2.Infrastructure.DependencyInjection.Registrations
{
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Services.BusinessDomain;

    using Microsoft.Practices.Unity;

    public static class EOS2Common
    {
        public static void Register(IUnityContainer container)
        {
             // Logging
            container.RegisterType<ILoggerService, LoggerService>();           
        }
    }
}
