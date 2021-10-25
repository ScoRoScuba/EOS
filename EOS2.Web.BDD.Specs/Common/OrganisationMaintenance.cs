namespace EOS2.Web.BDD.Specs.Common
{
    using System;
    using System.Linq;

    using EOS2.Common.Exceptions;
    using EOS2.Identity.Model;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.SetUp;

    using Microsoft.AspNet.Identity;
    using Microsoft.Practices.Unity;

    public class OrganizationMaintenance
    {
        public static User User(string userName)
        {
            var userIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityUserService>();
            var user = userIdentityService.FindByName(userName);
            return user;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Using IdentityUser<int, UserLogin, UserRole, UserClaim> is rediculous in this case.")]
        public static OrganizationRole PortalAgentOrganizationRole(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            var organizationService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();

            var roles = organizationService.GetUsersOrganizationalRoles(user.Id);

            var organizationRole = roles.First(uor => uor.OrganizationType == OrganizationType.PortalAgent);

            return organizationRole;
        }

        public static Organization AddCustomer(Organization organization)
        {
            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService != null)
            {
                organization = AddCustomer(organizationsService, organization);
            }

            return organization;
        }

        public static Organization[] AddCustomers(Organization[] customers)
        {
            if (customers == null) throw new ArgumentNullException("customers");

            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            foreach (var organization in customers)
            {
                AddCustomer(organizationsService, organization);
            }

            return customers;
        }

        public static Organization AddPortalAgent(Organization organization)
        {
            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            return AddPortalAgent(organizationsService, organization);
        }

        public static Organization AddPortalAgent(Organization organization, int parentOrganizationId)
        {
            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            organizationsService.SavePortalAgentOrganization(organization);  // TODO : Need to be able to assign - , parentOrganizationId);

            return organization;
        }

        public static Organization[] AddPortalAgents(Organization[] customers)
        {
            if (customers == null) throw new ArgumentNullException("customers");

            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            foreach (var organization in customers)
            {
                AddPortalAgent(organizationsService, organization);
            }

            return customers;
        }

        public static Organization AddServiceProvider(Organization organization)
        {
            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            return AddServiceProvider(organizationsService, organization);
        }

        public static Organization AddServiceProvider(Organization organization, int parentOrganizationId)
        {
            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService != null)
            {
                organizationsService.SaveServiceProviderOrganization(organization, parentOrganizationId);
            }

            return organization;
        }

        public static Organization[] AddServiceProviders(Organization[] customers)
        {
            if (customers == null) throw new ArgumentNullException("customers");

            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            foreach (var organization in customers)
            {
                AddServiceProvider(organizationsService, organization);
            }

            return customers;
        }

        public static Organization AddOwner(Organization organization)
        {
            var organizationsService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            if (organizationsService == null)
            {
                throw new DependencyResolutionException(typeof(IOrganizationsService), "organizationsService");
            }

            organizationsService.SaveOwnerOrganization(organization);

            return organization;
        }

        public static void ClearDownStartsWith(string startText)
        {
            var organizationRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Organization>>();
            var organizationRoleRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<OrganizationRole>>();
            var organizations = organizationRepository.GetAll().Where(o => o.Name.StartsWith(startText, StringComparison.OrdinalIgnoreCase));
            var organizationRoles = organizationRoleRepository.GetAll().Where(or => organizations.Any(o => o.Id == or.OrganizationId));
            foreach (var organizationRole in organizationRoles)
            {
                organizationRoleRepository.Remove(organizationRole);
            }

            foreach (var organization in organizations)
            {
                organizationRepository.Remove(organization);
            }
        }

        public static void ClearDownContaining(string text)
        {
            var organizationRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Organization>>();
            var organizationRoleRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<OrganizationRole>>();
            var organizations = organizationRepository.GetAll().Where(o => o.Name.Contains(text));
            var organizationRoles = organizationRoleRepository.GetAll().Where(or => organizations.Any(o => o.Id == or.OrganizationId));
            foreach (var organizationRole in organizationRoles)
            {
                organizationRoleRepository.Remove(organizationRole);
            }

            foreach (var organization in organizations)
            {
                organizationRepository.Remove(organization);
            }
        }

        public static void ClearDownUsers(string[] userNames)
        {
            var userIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityUserService>();
            foreach (var user in userNames.Select(userIdentityService.FindByName).Where(user => user != null))
            {
                userIdentityService.Delete(user);
            }
        }

        private static Organization AddCustomer(IOrganizationsService organizationsService, Organization organization)
        {
            organizationsService.SaveCustomerOrganization(organization);
            return organization;
        }

        private static Organization AddPortalAgent(IOrganizationsService organizationsService, Organization organization)
        {
            organizationsService.SavePortalAgentOrganization(organization);
            return organization;
        }

        private static Organization AddServiceProvider(IOrganizationsService organizationsService, Organization organization)
        {
            organizationsService.SaveServiceProviderOrganization(organization);
            return organization;
        }
    }
}
