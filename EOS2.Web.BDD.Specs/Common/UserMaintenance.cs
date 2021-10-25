namespace EOS2.Web.BDD.Specs.Common
{
    using System.Linq;

    using EOS2.Identity.Model;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.SetUp;

    using Microsoft.AspNet.Identity;
    using Microsoft.Practices.Unity;

    public static class UserMaintenance
    {
        private const string DefaultPassword = "!12345678A";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Code will not be compliled by a compiler that ignore default values(C++, VB)")]
        public static void CreateUser(string userName, string password = DefaultPassword)
        {
            var userIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityUserService>();

            var user = new User { UserName = userName, EmailConfirmed = true };
            userIdentityService.Create(user);

            userIdentityService.AddPassword(userIdentityService.FindByName(userName).Id, password);
        }

        public static void CreateRole(string roleName)
        {
            var roleIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityRoleService>();

            if (!roleIdentityService.RoleExists(roleName))
            {
                var role = new Role { Name = roleName };
                roleIdentityService.Create(role);
            }
        }

        public static void CreateUserRole(string userName,  string roleName)
        {
            var userIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityUserService>();
            var user = userIdentityService.FindByName(userName);
            userIdentityService.AddToRole(user.Id, roleName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Code will not be compliled by a compiler that ignore default values(C++, VB)")]
        public static void CreateUserAndRole(string userName, string roleName, OrganizationType organizationType, string password = DefaultPassword)
        {
            var organization = CreateOrganization(organizationType, null);
            CreateUser(userName, password);
            CreateRole(roleName);
            CreateUserRole(userName, roleName);
            CreateOrganizationRoleUser(userName, organization);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "portalAgentParent", Justification = "Will be used correctly when fixes made to called methods")] 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "customerParent", Justification = "Will be used correctly when fixes made to called methods")]
        private static Organization CreateOrganization(OrganizationType organizationType, int? parentOrganizationId)
        {
            var organization = new Organization { Name = organizationType + " Organization" };
            switch (organizationType)
            {
                case OrganizationType.Customer:
                    var customerParent = CreateOrganization(OrganizationType.ServiceProvider, null);
                    //// TODO: This should assign to parent but cant do that at the moment (TRAC 627 will add that functionality in, this can then be corrected)                    
                    //// return OrganizationMaintenance.AddCustomer(organization, customerParent.OrganizationRole.First(r => r.OrganizationType == OrganizationType.ServiceProvider).Id);
                    return OrganizationMaintenance.AddCustomer(organization);
                case OrganizationType.ServiceProvider:
                    var serviceProviderparent = CreateOrganization(OrganizationType.PortalAgent, null);
                    return OrganizationMaintenance.AddServiceProvider(organization, serviceProviderparent.OrganizationRole.First(r => r.OrganizationType == OrganizationType.PortalAgent).Id);
                case OrganizationType.PortalAgent:
                    var portalAgentParent = CreateOrganization(OrganizationType.EOSOwner, null);
                    //// TODO: This should assign to parent but cant do that at the moment.
                    return OrganizationMaintenance.AddPortalAgent(organization);
                case OrganizationType.EOSOwner:
                    return OrganizationMaintenance.AddOwner(organization);
            }

            return null; 
        }

        private static void CreateOrganizationRoleUser(string userName, Organization organization)
        {
            var userIdentityService = BeforeAfterTests.DependencyContainer.Resolve<IdentityUserService>();
            var organizationService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();
            
            organizationService.AddUserToOrganization(
                organization.Id,
                OrganizationType.ServiceProvider,
                userIdentityService.FindByName(userName).Id);
        }
    }
}
