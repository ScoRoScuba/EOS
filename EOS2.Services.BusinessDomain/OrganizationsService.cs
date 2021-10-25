namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Model.Enums;

    public class OrganizationsService : IOrganizationsService
    {
        private readonly IRepository<Organization> organizationRepository;
        private readonly IRepository<OrganizationRole> organizationRoleRepository;
        private readonly IRepository<OrganizationRoleUser> organizationRoleUserRepository;

        public OrganizationsService(IRepository<Organization> organizationRepository, IRepository<OrganizationRole> organizationRoleRepository, IRepository<OrganizationRoleUser> organizationRoleUserRepository)
        {
            if (organizationRepository == null) throw new ArgumentNullException("organizationRepository");
            if (organizationRoleRepository == null) throw new ArgumentNullException("organizationRoleRepository");
            if (organizationRoleUserRepository == null) throw new ArgumentNullException("organizationRoleUserRepository");

            this.organizationRepository = organizationRepository;
            this.organizationRoleRepository = organizationRoleRepository;
            this.organizationRoleUserRepository = organizationRoleUserRepository;
        }

        public bool OrganizationExists(string organizationName)
        {
            return organizationRepository.Find(
                    o => o.Name.ToLower().Trim() == organizationName.ToLower().Trim()) != null;            
        }

        public bool OrganizationExists(string organizationName, int organizationToIgnore)
        {
            return organizationRepository.Find(
                    o => o.Name.ToLower().Trim() == organizationName.ToLower().Trim() &&
                        o.Id != organizationToIgnore) != null;                        
        }

        public Organization GetCustomerOrganization(int customerId)
        {
            var role =
                organizationRoleRepository.Find(
                    or => or.OrganizationType == OrganizationType.Customer && or.Organization.Id == customerId);

            if (role != null)
            {
                return role.Organization;
            }

            return null;
        }

        public Organization GetOrganization(int organizationId)
        {
            return organizationRepository.Find(or => or.Id == organizationId);
        }

        public Organization GetPortalAgentOrganization(int portalAgentId)
        {
            return GetOrganization(OrganizationType.PortalAgent, portalAgentId);
        }

        public Organization GetServiceProviderOrganization(int serviceProviderId)
        {
            return GetOrganization(OrganizationType.ServiceProvider, serviceProviderId);
        }

        private Organization GetOrganization(OrganizationType organizationType, int organizationId)
        {
            var role =
                organizationRoleRepository.Find(or => or.OrganizationType == organizationType && or.Organization.Id == organizationId);

            return role != null ? role.Organization : null;
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
            return organizationRepository.GetAll();
        }

        public IEnumerable<Organization> GetAllOrganizationsOfType(OrganizationType organizationType, int parentOrganizationId)
        {
             var results = organizationRoleRepository.FindAll(or => or.OrganizationType == organizationType && or.ParentOrganizationId == parentOrganizationId);

            if (results == null) return new List<Organization>();

            return results.Select(r => r.Organization);   
        }

        public IEnumerable<Organization> GetAllOrganizationsOfType(OrganizationType organizationType)
        {
            var results = organizationRoleRepository.FindAll(or => or.OrganizationType == organizationType);

            if (results == null) return new List<Organization>();

            return results.Select(r => r.Organization);     
        }

        public IEnumerable<OrganizationRoleUser> GetUsersForOrganizationType(OrganizationType organizationType)
        {
            return organizationRoleUserRepository.FindAll(or => or.Role.OrganizationType == organizationType);
        }

        public IEnumerable<OrganizationRole> GetUsersOrganizationalRoles(int userId)
        {
            var usersOrganizationRoles = organizationRoleUserRepository.FindAll(oru => oru.UserId == userId);

            // ReSharper disable PossibleMultipleEnumeration
            if (!usersOrganizationRoles.Any())
            {
                return new List<OrganizationRole>();
            }

            return usersOrganizationRoles.Select(u => u.Role);
            // ReSharper restore PossibleMultipleEnumeration
        }

        public IEnumerable<OrganizationRoleUser> GetUsers(int organizationId, OrganizationType organizationType)
        {
            return organizationRoleUserRepository.FindAll(or => or.Role.OrganizationId == organizationId && or.Role.OrganizationType == organizationType);
        }

        public ServiceResultDictionary SavePortalAgentOrganization(Organization portalAgentOrganization)
        {
            if (portalAgentOrganization == null) throw new ArgumentNullException("portalAgentOrganization");

            var serviceResult = new ServiceResultDictionary();

            if (portalAgentOrganization.Id > 0)
            {
                this.organizationRepository.Update(portalAgentOrganization);
            }
            else
            {
                portalAgentOrganization.OrganizationRole.Add(
                    new OrganizationRole() { Name = portalAgentOrganization.Name, FromDate = DateTime.Now, OrganizationType = OrganizationType.PortalAgent });

                this.organizationRepository.Add(portalAgentOrganization);
            }

            return serviceResult;
        }

        public ServiceResultDictionary SaveOwnerOrganization(Organization ownerOrganization)
        {
            if (ownerOrganization == null) throw new ArgumentNullException("ownerOrganization");

            var serviceResult = new ServiceResultDictionary();

            if (ownerOrganization.Id > 0)
            {
                this.organizationRepository.Update(ownerOrganization);
            }
            else
            {
                ownerOrganization.OrganizationRole.Add(
                    new OrganizationRole() { Name = ownerOrganization.Name, FromDate = DateTime.Now, OrganizationType = OrganizationType.EOSOwner });

                this.organizationRepository.Add(ownerOrganization);
            }

            return serviceResult;
        }

        public ServiceResultDictionary AddUserToOrganization(int organizationId, OrganizationType organizationType, int userId)
        {
            if (organizationId == 0) throw new ArgumentNullException("organizationId");
            if (userId == 0) throw new ArgumentNullException("userId");

            var serviceResult = new ServiceResultDictionary();

            var organizationRole = organizationRoleRepository.Find(r => r.Organization.Id == organizationId && r.OrganizationType == organizationType);

            if (organizationRole == null)
            {
                serviceResult.AddModelError("organization", new ArgumentException("Unknown OrganizationRole"));
                return serviceResult; 
            }

            if (organizationRole.RoleUsers == null)
            {
                organizationRole.RoleUsers = new List<OrganizationRoleUser>();
            }

            organizationRole.RoleUsers.Add(new OrganizationRoleUser() { UserId = userId });

            organizationRoleRepository.Update(organizationRole);

            return serviceResult; 
        }

        public ServiceResultDictionary SaveServiceProviderOrganization(Organization serviceProviderOrganization)
        {
             if (serviceProviderOrganization == null) throw new ArgumentNullException("serviceProviderOrganization");

            var serviceResult = new ServiceResultDictionary();

            if (serviceProviderOrganization.Id > 0)
            {
                this.organizationRepository.Update(serviceProviderOrganization);
            }
            else
            {
                serviceProviderOrganization.OrganizationRole.Add(
                    new OrganizationRole() { Name = serviceProviderOrganization.Name, FromDate = DateTime.Now, OrganizationType = OrganizationType.ServiceProvider });

                this.organizationRepository.Add(serviceProviderOrganization);
            }

            return serviceResult;
        }

        public ServiceResultDictionary SaveServiceProviderOrganization(Organization serviceProviderOrganization, int parentOrganizationId)
        {
            if (serviceProviderOrganization == null) throw new ArgumentNullException("serviceProviderOrganization");

            var serviceResult = new ServiceResultDictionary();

            if (serviceProviderOrganization.Id > 0)
            {
                this.organizationRepository.Update(serviceProviderOrganization);
            }
            else
            {
                serviceProviderOrganization.OrganizationRole.Add(
                    new OrganizationRole
                                    { 
                                        Name = serviceProviderOrganization.Name, 
                                        FromDate = DateTime.Now, 
                                        OrganizationType = OrganizationType.ServiceProvider,
                                        ParentOrganizationId = parentOrganizationId
                                    });

                this.organizationRepository.Add(serviceProviderOrganization);
            }

            return serviceResult;
        }

        public ServiceResultDictionary SaveCustomerOrganization(Organization customerOrganization)
        {
            if (customerOrganization == null) throw new ArgumentNullException("customerOrganization");

            var serviceResult = new ServiceResultDictionary();

            if (customerOrganization.Id > 0)
            {
                this.organizationRepository.Update(customerOrganization);
            }
            else
            {
                customerOrganization.OrganizationRole.Add(
                    new OrganizationRole() { Name = customerOrganization.Name, FromDate = DateTime.Now, OrganizationType = OrganizationType.Customer });

                this.organizationRepository.Add(customerOrganization);
            }

            return serviceResult;
        }
    }
}