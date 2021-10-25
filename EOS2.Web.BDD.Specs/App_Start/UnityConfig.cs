namespace EOS2.Web.BDD.Specs.App_Start
{
    using System;
    using System.Data.Entity;

    using EOS2.Identity.Model;
    using EOS2.Identity.Repository;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Repository;

    using Microsoft.AspNet.Identity;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer ConfiguredContainer
        {
            get
            {
                return Container.Value;
            }
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Standard call for Unity.")] 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "container", Justification = "Standard call for Unity.")]
        public static void RegisterTypes(IUnityContainer container)
        {
            // replace this one with one of your own
            Database.SetInitializer<EOS2DataContext>(null);
            Database.SetInitializer<EOSIdentityDbContext>(null);

            // Repository
            container.RegisterType<IDataContext, EOS2DataContext>(new ContainerControlledLifetimeManager(), new InjectionConstructor("EOS2Database"));

            // Identity
            container.RegisterType<EOSIdentityDbContext, EOSIdentityDbContext>(new ContainerControlledLifetimeManager(), new InjectionConstructor("EOS2Database"));

            container.RegisterType<IRoleStore<Role, int>, IdentityRolesRepository>();
            container.RegisterType<IUserStore<User, int>, UserRepository>();            

            Infrastructure.DependencyInjection.Registrations.Repository.Register(container);
            Infrastructure.DependencyInjection.Registrations.EOS2Services.Register(container);     
        }
    }
}
