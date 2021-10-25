namespace EOS2.Web.Tests.TestStubs
{
    using System;
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Model.Enums;

    public class StubOrganizationService : IOrganizationsService
    {
        private readonly bool organizationExistsReturnValue;

        public StubOrganizationService(bool organizationExistsReturnValue)
        {
            this.organizationExistsReturnValue = organizationExistsReturnValue;
        }

        public bool OrganizationExistsReturnValue { get; set; }

        // TODO: Remove this suppression when the method is implemented.
        public static ServiceResultDictionary AddUserToOrganization(OrganizationRole organizationRole, int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary SaveCustomerOrganization(Organization customerOrganization)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary SaveOwnerOrganization(Organization ownerOrganization)
        {
            throw new NotImplementedException();
        }

        public Organization GetCustomerOrganization(int customerId)
        {
            throw new NotImplementedException();
        }

        public bool OrganizationExists(string organizationName)
        {
            return organizationExistsReturnValue;
        }

        public bool OrganizationExists(string organizationName, int organizationToIgnore)
        {
            return organizationExistsReturnValue;
        }

        public IEnumerable<OrganizationRole> GetUsersOrganizationalRoles(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetAllOrganizationsOfType(OrganizationType organizationType)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary SavePortalAgentOrganization(Organization portalAgentOrganization)
        {
            throw new NotImplementedException();
        }

        public Organization GetPortalAgentOrganization(int portalAgentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrganizationRoleUser> GetUsersForOrganizationType(OrganizationType organizationType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrganizationRoleUser> GetUsers(int organizationId, OrganizationType organizationType)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary AddUserToOrganization(int organizationId, OrganizationType organizationType, int userId)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary SaveServiceProviderOrganization(Organization serviceProviderOrganization, int parentOrganizationId)
        {
            throw new NotImplementedException();
        }

        public Organization GetServiceProviderOrganization(int serviceProviderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetAllOrganizationsOfType(OrganizationType organizationType, int parentOrganizationId)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary SaveServiceProviderOrganization(Organization serviceProviderOrganization)
        {
            throw new NotImplementedException();
        }

        public Organization GetOrganization(int organizationId)
        {
            throw new NotImplementedException();
        }
    }
}
