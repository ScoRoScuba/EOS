namespace EOS2.Infrastructure.DependencyInjection.Registrations
{
    using System.Data.Entity;
    using System.Web.UI.WebControls;

    using EOS2.Identity.Model;
    using EOS2.Identity.Repository;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Repository;

    using Microsoft.AspNet.Identity;
    using Microsoft.Practices.Unity;

    public static class DataStore
    {
        public static void Register(IUnityContainer container)
        {
            // These should not really be here but having them in the GLOBAL ASA means 
            // refering to the Repository Assemblies in the web project which is not 
            // really a good design choice
            Database.SetInitializer<EOSIdentityDbContext>(null);
            Database.SetInitializer<EOS2DataContext>(null);

            // Identity
            container.RegisterType<EOSIdentityDbContext, EOSIdentityDbContext>(new PerRequestLifetimeManager(), new InjectionConstructor("EOS2Database"));

            // Repository
            container.RegisterType<IDataContext, EOS2DataContext>(new PerRequestLifetimeManager(), new InjectionConstructor("EOS2Database"));

            container.RegisterType<IRoleStore<Role, int>, IdentityRolesRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<User, int>, UserRepository>(new PerRequestLifetimeManager());            

            container.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
