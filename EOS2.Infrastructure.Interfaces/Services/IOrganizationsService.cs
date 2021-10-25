namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;
    using EOS2.Model.Enums;

    public interface IOrganizationsService
    {
        bool OrganizationExists(string organizationName);

        bool OrganizationExists(string organizationName, int organizationToIgnore);

        Organization GetOrganization(int organizationId);

        Organization GetCustomerOrganization(int customerId);

        Organization GetPortalAgentOrganization(int portalAgentId);

        Organization GetServiceProviderOrganization(int serviceProviderId);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is a method declaration and not Property")]
        IEnumerable<Organization> GetAllOrganizations();        

        IEnumerable<Organization> GetAllOrganizationsOfType(OrganizationType organizationType);

        IEnumerable<Organization> GetAllOrganizationsOfType(OrganizationType organizationType, int parentOrganizationId);
        
        IEnumerable<OrganizationRoleUser> GetUsersForOrganizationType(OrganizationType organizationType);

        IEnumerable<OrganizationRole> GetUsersOrganizationalRoles(int userId);

        IEnumerable<OrganizationRoleUser> GetUsers(int organizationId, OrganizationType organizationType);
        
        ServiceResultDictionary AddUserToOrganization(int organizationId, OrganizationType organizationType, int userId);

        ServiceResultDictionary SavePortalAgentOrganization(Organization portalAgentOrganization);

        ServiceResultDictionary SaveServiceProviderOrganization(Organization serviceProviderOrganization);

        ServiceResultDictionary SaveServiceProviderOrganization(Organization serviceProviderOrganization, int parentOrganizationId);

        ServiceResultDictionary SaveCustomerOrganization(Organization customerOrganization);

        ServiceResultDictionary SaveOwnerOrganization(Organization ownerOrganization);
    }
}
