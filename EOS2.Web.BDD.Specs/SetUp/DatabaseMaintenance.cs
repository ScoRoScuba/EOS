namespace EOS2.Web.BDD.Specs.SetUp
{
    using System.Linq;

    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;

    using Microsoft.AspNet.Identity;
    using Microsoft.Practices.Unity;

    using TechTalk.SpecFlow;

    public class DatabaseMaintenance
    {
        public static void Reset()
        {
            Reset(false);
        }

        public static void Reset(bool forceReset)
        {
            if (!ScenarioContext.Current.ContainsKey("IsDatabaseReset") || forceReset)
            {
                // Organization Related
                var instrumentRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Instrument>>();
                var equipmentRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Equipment>>();
                var scheduleRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Schedule>>();
                var plantAreaRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<PlantArea>>();
                var siteRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Site>>();
                var certificateRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<CertificateHeader>>();
                var channelsRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Channel>>();
                var organizationRoleUserRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<OrganizationRoleUser>>();
                var organizationRoleRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<OrganizationRole>>();
                var organizationRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Organization>>();

                certificateRepository.GetAll().ToList().ForEach(item => certificateRepository.Remove(item));
                scheduleRepository.GetAll().ToList().ForEach(item => scheduleRepository.Remove(item));
                channelsRepository.GetAll().ToList().ForEach(item => channelsRepository.Remove(item));
                instrumentRepository.GetAll().ToList().ForEach(item => instrumentRepository.Remove(item));
                equipmentRepository.GetAll().ToList().ForEach(item => equipmentRepository.Remove(item));
                plantAreaRepository.GetAll().ToList().ForEach(item => plantAreaRepository.Remove(item));
                siteRepository.GetAll().ToList().ForEach(item => siteRepository.Remove(item));
                organizationRoleUserRepository.GetAll().ToList().ForEach(item => organizationRoleUserRepository.Remove(item));
                organizationRoleRepository.GetAll().ToList().ForEach(item => organizationRoleRepository.Remove(item));
                organizationRepository.GetAll().ToList().ForEach(item => organizationRepository.Remove(item));

                // User Related
                var userIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityUserService>();
                var roleIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityRoleService>();

                userIdentityService.Users.ToList().ForEach(item => userIdentityService.Delete(item));
                roleIdentityService.Roles.ToList().ForEach(item => roleIdentityService.Delete(item));

                // Set completed flag (to avoid unintentional re-runs)
                ScenarioContext.Current.Add("IsDatabaseReset", true);
            }
        }
    }
}
