namespace EOS2.Services.Tests.OrganizationServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Model.Enums;
    using EOS2.Services.BusinessDomain;

    using Moq;
    using NUnit.Framework;

    using Assert = NUnit.Framework.Assert;

    public abstract class OrganizationServiceTestsBase
    {
        protected Mock<IRepository<Organization>> MockOrganizationRepository { get; set; }

        protected Mock<IRepository<OrganizationRole>> MockOrganizationRoleRepository { get; set; }

        protected Mock<IRepository<OrganizationRoleUser>> MockOrganizationRoleUserRepository { get; set; }
        
        [SetUp]
        public void FixtureSetup()
        {
            this.MockOrganizationRepository = new Mock<IRepository<Organization>>();
            this.MockOrganizationRoleRepository = new Mock<IRepository<OrganizationRole>>();
            this.MockOrganizationRoleUserRepository = new Mock<IRepository<OrganizationRoleUser>>();
        }

        public IOrganizationsService ServiceUnderTest()
        {
            return new OrganizationsService(this.MockOrganizationRepository.Object, this.MockOrganizationRoleRepository.Object, this.MockOrganizationRoleUserRepository.Object);
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class GetCustomerOrganizationMethod : OrganizationServiceTestsBase
    {
        [Test]
        public void ReturnsCustomerOrganizationIfFound()
        {
            // ARRANGE
            var organizationList = new List<OrganizationRole>()
                                        {
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.Customer,
                                                    Organization = new Organization()
                                                                        {
                                                                            Id = 1,
                                                                            Name = "Customer Organization"
                                                                        }
                                                },
                                            new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner },
                                            new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider }
                                        };

            OrganizationRole foundOrganization = null;

            MockOrganizationRoleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<OrganizationRole, bool>>>()))
                .Callback(
                    (Expression<Func<OrganizationRole, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            const int CustomerOrganizationIdToUse = 1;

            var organizationService = this.ServiceUnderTest();

            // ACT 
            var result = organizationService.GetCustomerOrganization(CustomerOrganizationIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsNullIfOrganizationNotFound()
        {
            // ARRANGE
            var organizationList = new List<OrganizationRole>()
                                        {
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.Customer,
                                                    Organization = new Organization()
                                                                        {
                                                                            Id = 1,
                                                                            Name = "Customer Organization"
                                                                        }
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.EOSOwner,
                                                    Organization = new Organization()
                                                                        {
                                                                            Id = 2,
                                                                            Name = "EOS Owner Organization"
                                                                        }
                                                },
                                            new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider }
                                        };

            OrganizationRole foundOrganization = null;

            MockOrganizationRoleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<OrganizationRole, bool>>>()))
                .Callback(
                    (Expression<Func<OrganizationRole, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            const int CustomerOrganizationIdToUse = 2;
            var organizationService = new OrganizationsService(MockOrganizationRepository.Object, MockOrganizationRoleRepository.Object, MockOrganizationRoleUserRepository.Object);

            // ACT 
            var result = organizationService.GetCustomerOrganization(CustomerOrganizationIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);

            MockOrganizationRoleRepository.Verify();
        }
    }

    [TestFixture]
    public class SaveCustomerOrganizationMethod : OrganizationServiceTestsBase
    {
        [Test]
        public void AddsNewOrganizationToRepository()
        {
            // Arrange
            var organizationList = new List<Organization>();
            var organizationToAdd = new Organization();

            MockOrganizationRepository.Setup(a => a.Add(It.IsAny<Organization>()))
                .Callback((Organization org) => organizationList.Add(org));

            MockOrganizationRepository.Setup(a => a.GetAll()).Returns(organizationList);

            var organizationService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            organizationService.SaveCustomerOrganization(organizationToAdd);

            // We check the Organization List as this is our actual data store
            Assert.That(organizationList, Is.Not.Empty);
        }

        [Test]
        public void AddsCorrectOrganizationType()
        {
            // Arrange
            var organizationList = new List<Organization>();
            var organizationToAdd = new Organization();

            // Entity Framework automatically loads the Id for us, here we have to make sure we have one and its added to our list
            MockOrganizationRepository.Setup(a => a.Add(It.IsAny<Organization>()))
                .Callback(
                    (Organization org) =>
                        {
                            org.Id = 1;
                            organizationList.Add(org);
                        });

            MockOrganizationRepository.Setup(a => a.GetAll()).Returns(organizationList);

            var organizationService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            organizationService.SaveCustomerOrganization(organizationToAdd);

            // Assert ( Test's worked )                
            Assert.That(organizationToAdd.OrganizationRole, Is.Not.Empty);
            Assert.That(organizationToAdd.OrganizationRole.Any(m => m.OrganizationType == OrganizationType.Customer), Is.True);
        }

        [Test]
        public void UpdateOrganizationCorrectly()
        {
            // Arrange
            var organizationList = new List<Organization>()
                                        {
                                            new Organization()
                                                {
                                                    Id = 1,
                                                    Name = "1st Organization"
                                                },
                                            new Organization()
                                                {
                                                    Id = 2,
                                                    Name = "2nd Organization"
                                                }
                                        };
            var organizationToUpdate = new Organization()
                                        {
                                            Id = 1,
                                            Name = "1st Organization Renamed"
                                        };

            // Entity Framework automatically loads the Id for us, here we have to make sure we have one and its added to our list
            MockOrganizationRepository.Setup(a => a.Update(It.IsAny<Organization>()))
                .Callback((Organization org) => { organizationList.Single(s => s.Id == org.Id).Name = org.Name; });

            var organizationService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            organizationService.SaveCustomerOrganization(organizationToUpdate);

            // Assert ( Test its worked )
            Assert.That(organizationList.Single(s => s.Id == 1).Name, Is.EqualTo(organizationToUpdate.Name));
        }
    }

    [TestFixture]
    public class GetAllOrganizationsMethod : OrganizationServiceTestsBase
    {
        [Test]
        public void ReturnsAllOrganizations()
        {
            var organizationList = new List<Organization>()
                                        {
                                            new Organization(),
                                            new Organization(),
                                            new Organization()
                                        };

            MockOrganizationRepository.Setup(a => a.GetAll()).Returns(organizationList);

            var organizationService = this.ServiceUnderTest();

            var organizations = organizationService.GetAllOrganizations();

            Assert.That(organizations, Is.Not.Empty);
        }
    }

    [TestFixture]
    public class OrganizationExistsMethods : OrganizationServiceTestsBase
    {
        [Test]
        public void ReturnsTrueIfAddingOrganizationWithSameName()
        {
            var organizationList = new List<Organization>()
                                        {
                                                new Organization { Id = 1, Name = "Organization" }
                                        };

            Organization foundOrganization = null;

            MockOrganizationRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Organization, bool>>>()))
                .Callback(
                    (Expression<Func<Organization, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            const string OrganizationNameToUse = "Organization";
            var organizationService = this.ServiceUnderTest();

            // ACT 
            var result = organizationService.OrganizationExists(OrganizationNameToUse);

            // ASSERT                
            Assert.That(result, Is.True);

            MockOrganizationRepository.Verify();                
        }

        [Test]
        public void ReturnsFalseIfAddingOrganizationWithDifferentName()
        {
            var organizationList = new List<Organization>()
                                        {
                                            new Organization { Id = 1, Name = "Organization" }
                                        };
    
            Organization foundOrganization = null;

            MockOrganizationRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Organization, bool>>>()))
                .Callback(
                    (Expression<Func<Organization, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            const string OrganizationNameToUse = "New Organization";
            var organizationService = this.ServiceUnderTest();

            // ACT 
            var result = organizationService.OrganizationExists(OrganizationNameToUse);

            // ASSERT
            Assert.That(result, Is.False);

            MockOrganizationRepository.Verify();                
        }

        [Test]
        public void ReturnsTrueIfUpdatingOrganizationWithSameName()
        {
            var organizationList = new List<Organization>()
                                        {
                                            new Organization() { Id = 1, Name = "1st Organization" },
                                            new Organization() { Id = 1, Name = "2nd Organization" }
                                        };

            Organization foundOrganization = null;

            MockOrganizationRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Organization, bool>>>()))
                .Callback(
                    (Expression<Func<Organization, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            const string OrganizationNameToUse = "1st Organization";
            const int OrgainsaitionIdtoIgnore = 2;
            var organizationService = this.ServiceUnderTest();

            // ACT 
            var result = organizationService.OrganizationExists(OrganizationNameToUse, OrgainsaitionIdtoIgnore);

            // ASSERT
            Assert.That(result, Is.True);

            MockOrganizationRepository.Verify();   
        }

        [Test]
        public void ReturnsFalseIfUpdatingOrganizationWithDifferentName()
        {
            var organizationList = new List<Organization>()
                                        {
                                            new Organization() { Id = 1, Name = "1st Organization" },
                                            new Organization() { Id = 1, Name = "2nd Organization" }
                                        };

            Organization foundOrganization = null;

            MockOrganizationRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Organization, bool>>>()))
                .Callback(
                    (Expression<Func<Organization, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            const string OrganizationNameToUse = "2nd Organization Renamed";
            const int OrgainsaitionIdtoIgnore = 2;
            var organizationService = this.ServiceUnderTest();

            // ACT 
            var result = organizationService.OrganizationExists(OrganizationNameToUse, OrgainsaitionIdtoIgnore);

            // ASSERT
            Assert.That(result, Is.False);

            MockOrganizationRepository.Verify();   
        }
    }

    [TestFixture]
    public class GetPortalAgentOrganizationMethod : OrganizationServiceTestsBase
    {
        private IList<OrganizationRole> organizationList;

        private OrganizationRole foundOrganization = null;

        private IOrganizationsService organizationService;

        [SetUp]
        public void TestSetup()
        {
            organizationList = new List<OrganizationRole>()
                                        {
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.Customer,
                                                    Organization = new Organization()
                                                            {
                                                                Id = 1,
                                                                Name = "Customer Organization"
                                                            }
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.EOSOwner
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.PortalAgent,
                                                    Organization = new Organization()
                                                            {
                                                                Id = 3,
                                                                Name = "Portal Agent Organization"
                                                            }
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.ServiceProvider
                                                }
                                        };

            MockOrganizationRepository = new Mock<IRepository<Organization>>();
            MockOrganizationRoleRepository = new Mock<IRepository<OrganizationRole>>();
            MockOrganizationRoleUserRepository = new Mock<IRepository<OrganizationRoleUser>>();

            MockOrganizationRoleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<OrganizationRole, bool>>>()))
                .Callback(
                    (Expression<Func<OrganizationRole, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ReturnsPortalAgentOrganizationIfFound()
        {
            const int PortalAgentOrganizationIdToUse = 3;
                
            // ACT 
            var result = organizationService.GetPortalAgentOrganization(PortalAgentOrganizationIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(3));

            MockOrganizationRoleRepository.Verify();
        }

        [Test]
        public void ReturnsNullIfOrganizationNotFound()
        {
            const int PortalAgentOrganizationIdToUse = 2;

            // ACT 
            var result = organizationService.GetPortalAgentOrganization(PortalAgentOrganizationIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);

            MockOrganizationRoleRepository.Verify();
        }            
    }

    [TestFixture]
    public class SavePortalAgentOrganizationMethod : OrganizationServiceTestsBase
    {
        [Test]
        public void AddsNewOrganizationToRepository()
        {
            // Arrange
            var organizationList = new List<Organization>();
            var organizationToAdd = new Organization();

            MockOrganizationRepository.Setup(a => a.Add(It.IsAny<Organization>()))
                .Callback((Organization org) => organizationList.Add(org));

            var organizationService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            organizationService.SavePortalAgentOrganization(organizationToAdd);

            // We check the Organization List as this is our actual data store
            Assert.That(organizationList, Is.Not.Empty);
        }

        [Test]
        public void AddsCorrectOrganizationType()
        {
            // Arrange
            var organizationList = new List<Organization>();
            var organizationToAdd = new Organization();

            // Entity Framework automatically loads the Id for us, here we have to make sure we have one and its added to our list
            MockOrganizationRepository.Setup(a => a.Add(It.IsAny<Organization>()))
                .Callback(
                    (Organization org) =>
                        {
                            org.Id = 1;
                            organizationList.Add(org);
                        });

            MockOrganizationRepository.Setup(a => a.GetAll()).Returns(organizationList);

            var organizationService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            organizationService.SavePortalAgentOrganization(organizationToAdd);

            // Assert ( Test's worked )                
            Assert.That(organizationToAdd.OrganizationRole, Is.Not.Empty);
            Assert.That(organizationToAdd.OrganizationRole.Any(m => m.OrganizationType == OrganizationType.PortalAgent), Is.True);
        }

        [Test]
        public void UpdateOrganizationCorrectly()
        {
            // Arrange
            var organizationList = new List<Organization>()
                                        {
                                            new Organization()
                                                {
                                                    Id = 1,
                                                    Name = "1st Organization"
                                                },
                                            new Organization()
                                                {
                                                    Id = 2,
                                                    Name = "2nd Organization"
                                                }
                                        };
            var organizationToUpdate = new Organization()
                                        {
                                            Id = 1,
                                            Name = "1st Organization Renamed"
                                        };

            // Entity Framework automatically loads the Id for us, here we have to make sure we have one and its added to our list
            MockOrganizationRepository.Setup(a => a.Update(It.IsAny<Organization>()))
                .Callback((Organization org) => { organizationList.Single(s => s.Id == org.Id).Name = org.Name; });

            var organizationService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            organizationService.SavePortalAgentOrganization(organizationToUpdate);

            // Assert ( Test its worked )
            Assert.That(organizationList.Single(s => s.Id == 1).Name, Is.EqualTo(organizationToUpdate.Name));
        }            
    }

    [TestFixture]
    public class GetAllOrganizationsOfTypeMethod : OrganizationServiceTestsBase
    {
        private IList<OrganizationRole> organizationRoleList;
        private IOrganizationsService organizationService;

        private IList<OrganizationRole> filteredResults;

        [SetUp]
        public void TestSetup()
        {
            organizationRoleList = new List<OrganizationRole>()
                                        {
                                            new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() },
                                        };                

            MockOrganizationRoleRepository.Setup(a => a.FindAll(It.IsAny<Expression<Func<OrganizationRole, bool>>>())).Callback( 
                (Expression<Func<OrganizationRole, bool>> pred) => filteredResults = organizationRoleList.AsQueryable().Where(pred).ToList()).Returns(() => filteredResults);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ReturnsNullIfNoOrganizationFound()
        {
            organizationRoleList = new List<OrganizationRole>();                

            MockOrganizationRoleRepository.Setup(a => a.FindAll(It.IsAny<Expression<Func<OrganizationRole, bool>>>())).Callback( 
                (Expression<Func<OrganizationRole, bool>> pred) => filteredResults = organizationRoleList.AsQueryable().Where(pred).ToList()).Returns(() => filteredResults);

            organizationService = this.ServiceUnderTest();

            var organizations = organizationService.GetAllOrganizationsOfType(OrganizationType.EOSOwner);

            Assert.That(organizations, Is.Empty);
        }

        [Test]
        public void ReturnsEOSOwnerOrg()
        {
            var organizations = organizationService.GetAllOrganizationsOfType(OrganizationType.EOSOwner);

            Assert.That(organizations, Is.Not.Empty);
            Assert.That(organizations.Count(), Is.EqualTo(1));
        }
                            
        [Test]
        public void ReturnsAllPortalAgentOrganizations()
        {
            var organizations = organizationService.GetAllOrganizationsOfType(OrganizationType.PortalAgent);

            Assert.That(organizations, Is.Not.Empty);
            Assert.That(organizations.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsAllServiceProviderOrganizations()
        {
            var organizations = organizationService.GetAllOrganizationsOfType(OrganizationType.ServiceProvider);

            Assert.That(organizations, Is.Not.Empty);
            Assert.That(organizations.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ReturnsAllCustomerOrganizations()
        {
            var organizations = organizationService.GetAllOrganizationsOfType(OrganizationType.Customer);

            Assert.That(organizations, Is.Not.Empty);
            Assert.That(organizations.Count(), Is.EqualTo(4));
        }
    }

    [TestFixture]
    public class GetUsersOrganizationalRolesMethod : OrganizationServiceTestsBase
    {
        private IOrganizationsService organizationService;

        private IList<OrganizationRoleUser> usersOrganisationalRoles;

        private IList<OrganizationRoleUser> filteredResults;

        [SetUp]
        public void TestSetup()
        {
            usersOrganisationalRoles = new List<OrganizationRoleUser>()
                                            {
                                                new OrganizationRoleUser() { Id = 1, Role = new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner }, UserId = 1 },
                                                new OrganizationRoleUser() { Id = 2, Role = new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent }, UserId = 1 },
                                                new OrganizationRoleUser() { Id = 3, Role = new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider }, UserId = 2 },
                                                new OrganizationRoleUser() { Id = 4, Role = new OrganizationRole() { OrganizationType = OrganizationType.Customer }, UserId = 3 },
                                            };

            MockOrganizationRoleUserRepository.Setup(a => a.FindAll(It.IsAny<Expression<Func<OrganizationRoleUser, bool>>>())).Callback( 
                (Expression<Func<OrganizationRoleUser, bool>> pred) => filteredResults = usersOrganisationalRoles.AsQueryable().Where(pred).ToList()).Returns(() => filteredResults);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ReturnsCorrectRolesForUserIdOne()
        {
            const int UserId = 1;

            var result = organizationService.GetUsersOrganizationalRoles(UserId);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsCorrectRolesForUserIdTwo()
        {
            const int UserId = 2;

            var result = organizationService.GetUsersOrganizationalRoles(UserId);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class GetUsersForOrganizationTypeMethod : OrganizationServiceTestsBase
    {
        private IOrganizationsService organizationService;

        private IList<OrganizationRoleUser> usersOrganisationalRoles;

        private IList<OrganizationRoleUser> filteredResults;

        [SetUp]
        public void TestSetup()
        {
            usersOrganisationalRoles = new List<OrganizationRoleUser>()
                                            {
                                                new OrganizationRoleUser() { Id = 1, Role = new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner }, UserId = 1 },
                                                new OrganizationRoleUser() { Id = 1, Role = new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner }, UserId = 2 },
                                                new OrganizationRoleUser() { Id = 1, Role = new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner }, UserId = 3 },
                                                new OrganizationRoleUser() { Id = 2, Role = new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent }, UserId = 4 },
                                                new OrganizationRoleUser() { Id = 2, Role = new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent }, UserId = 5 },
                                                new OrganizationRoleUser() { Id = 3, Role = new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider }, UserId = 6 },
                                                new OrganizationRoleUser() { Id = 4, Role = new OrganizationRole() { OrganizationType = OrganizationType.Customer }, UserId = 7 },
                                                new OrganizationRoleUser() { Id = 4, Role = new OrganizationRole() { OrganizationType = OrganizationType.Customer }, UserId = 8 },
                                                new OrganizationRoleUser() { Id = 4, Role = new OrganizationRole() { OrganizationType = OrganizationType.Customer }, UserId = 9 },
                                                new OrganizationRoleUser() { Id = 4, Role = new OrganizationRole() { OrganizationType = OrganizationType.Customer }, UserId = 10 },
                                            };

            MockOrganizationRepository = new Mock<IRepository<Organization>>();
            MockOrganizationRoleRepository = new Mock<IRepository<OrganizationRole>>();
            MockOrganizationRoleUserRepository = new Mock<IRepository<OrganizationRoleUser>>();

            MockOrganizationRoleUserRepository.Setup(a => a.FindAll(It.IsAny<Expression<Func<OrganizationRoleUser, bool>>>())).Callback( 
                (Expression<Func<OrganizationRoleUser, bool>> pred) => filteredResults = usersOrganisationalRoles.AsQueryable().Where(pred).ToList()).Returns(() => filteredResults);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ReturnsUsersForOrganizationRole()
        {
            var eosOwnerResult = organizationService.GetUsersForOrganizationType(OrganizationType.EOSOwner);
            var portalAgentResult = organizationService.GetUsersForOrganizationType(OrganizationType.PortalAgent);
            var serviceProviderResult = organizationService.GetUsersForOrganizationType(OrganizationType.ServiceProvider);
            var customerResult = organizationService.GetUsersForOrganizationType(OrganizationType.Customer);

            Assert.That(eosOwnerResult, Is.Not.Empty);
            Assert.That(eosOwnerResult.Count(), Is.EqualTo(3));                

            Assert.That(portalAgentResult, Is.Not.Empty);
            Assert.That(portalAgentResult.Count(), Is.EqualTo(2));                

            Assert.That(serviceProviderResult, Is.Not.Empty);
            Assert.That(serviceProviderResult.Count(), Is.EqualTo(1));                

            Assert.That(customerResult, Is.Not.Empty);
            Assert.That(customerResult.Count(), Is.EqualTo(4));                
        }
    }

    [TestFixture]
    public class AddUserToOrganizationMethod : OrganizationServiceTestsBase
    {
        private IOrganizationsService organizationService;

        private IList<OrganizationRoleUser> usersOrganisationalRoles;

        private OrganizationRole organizationRole;

        [SetUp]
        public void TestSetup()
        {
            organizationRole = new OrganizationRole()
                                    {
                                        OrganizationType = OrganizationType.EOSOwner,
                                        Organization = new Organization() { Id = 1, Name = "EOS OWner" },
                                        RoleUsers = new List<OrganizationRoleUser>()                                           
                                    };

            usersOrganisationalRoles = new List<OrganizationRoleUser>()
                {
                    new OrganizationRoleUser()
                    {
                            Id = 1, 
                            Role = organizationRole,
                        UserId = 1                             
                    }
                };

            MockOrganizationRoleUserRepository.Setup(a => a.Add(It.IsAny<OrganizationRoleUser>())).Callback((OrganizationRoleUser org) => usersOrganisationalRoles.Add(org));

            MockOrganizationRoleRepository.Setup(a => a.Find(It.IsAny<Expression<Func<OrganizationRole, bool>>>())).Returns(organizationRole);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ThrowsArgumentNullExceptionForOrganizationId()
        {
            Assert.Throws<ArgumentNullException>(() => organizationService.AddUserToOrganization(0, OrganizationType.EOSOwner, 0));
        }

        [Test]
        public void ThrowsArgumentNullExceptionForZeroUserId()
        {
            Assert.Throws<ArgumentNullException>(() => organizationService.AddUserToOrganization(0, OrganizationType.EOSOwner, 0));
        }

        [Test]
        public void AddsUserIdToOrganizationRole()
        {
            organizationService.AddUserToOrganization(1, OrganizationType.EOSOwner, 2);

            Assert.That(organizationRole.RoleUsers.Count, Is.EqualTo(1));

            Assert.That(organizationRole.RoleUsers[0].UserId, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class SaveServiceProviderOrganizationWithParentOrganizationIdMethod : OrganizationServiceTestsBase
    {
        private IList<Organization> organizationStore;

        private IOrganizationsService organizationService;

        [SetUp]
        public void TestSetup()
        {
            organizationStore = new List<Organization>()
                                    {
                                        new Organization() { Id = 10, Name = "Org 1" }
                                    };

            MockOrganizationRoleRepository = new Mock<IRepository<OrganizationRole>>();

            MockOrganizationRepository = new Mock<IRepository<Organization>>();

            MockOrganizationRoleUserRepository = new Mock<IRepository<OrganizationRoleUser>>();

            MockOrganizationRepository.Setup(r => r.Add(It.IsAny<Organization>())).Callback((Organization org) => organizationStore.Add(org));

            MockOrganizationRepository.Setup(r => r.Update(It.IsAny<Organization>())).Callback(
                (Organization org) =>
                    {
                        organizationStore.Single(o => o.Id == org.Id).Name = org.Name;                           
                    });

            organizationService = this.ServiceUnderTest();
        }
                
        [Test]
        public void AddsServiceProviderToRepository()
        {
            var newOrganization = new Organization()
                                        {
                                            Name = "New Organization",
                                            PostalCode = "123456",
                                            Address = "Address1"
                                        };

            const int ParentOrganizationId = 1;

            organizationService.SaveServiceProviderOrganization(newOrganization, ParentOrganizationId);

            var itemAdded = organizationStore.SingleOrDefault(i => i.Name == newOrganization.Name);

            Assert.That(organizationStore, Is.Not.Empty);
            Assert.That(organizationStore.Count, Is.EqualTo(2));
            Assert.That(itemAdded, Is.Not.Null);
            Assert.That(itemAdded.OrganizationRole, Is.Not.Empty);
            Assert.That(itemAdded.OrganizationRole[0].OrganizationType, Is.EqualTo(OrganizationType.ServiceProvider));
            Assert.That(itemAdded.OrganizationRole[0].ParentOrganizationId, Is.EqualTo(ParentOrganizationId));
        }

        [Test]
        public void UpdatesServiceProviderInRepository()
        {
                var newOrganization = new Organization()
                                        {
                                            Id = 10,
                                            Name = "New Organization",
                                            PostalCode = "123456",
                                            Address = "Address1"
                                        };

            const int ParentOrganizationId = 1;

            organizationService.SaveServiceProviderOrganization(newOrganization, ParentOrganizationId);

            Assert.That(organizationStore[0].Name, Is.EqualTo(newOrganization.Name));
        }

        [Test]
        public void ThrowsArgumentNullExceptionWhenNullServiceProviderOrganization()
        {
            Assert.Throws<ArgumentNullException>(() => organizationService.SaveServiceProviderOrganization(null, 0));                
        }
    }

    [TestFixture]
    public class SaveServiceProviderOrganizationMethod : OrganizationServiceTestsBase
    {
        private IList<Organization> organizationStore;

        private IOrganizationsService organizationService;

        [SetUp]
        public void TestSetup()
        {
            organizationStore = new List<Organization>()
                                    {
                                        new Organization() { Id = 10, Name = "Org 1" }
                                    };

            MockOrganizationRepository.Setup(r => r.Add(It.IsAny<Organization>())).Callback((Organization org) => organizationStore.Add(org));

            MockOrganizationRepository.Setup(r => r.Update(It.IsAny<Organization>())).Callback(
                (Organization org) =>
                    {
                        organizationStore.Single(o => o.Id == org.Id).Name = org.Name;                           
                    });

            organizationService = this.ServiceUnderTest();
        }
                
        [Test]
        public void AddsServiceProviderToRepository()
        {
            var newOrganization = new Organization()
                                        {
                                            Name = "New Organization",
                                            PostalCode = "123456",
                                            Address = "Address1"
                                        };

            organizationService.SaveServiceProviderOrganization(newOrganization);

            var itemAdded = organizationStore.SingleOrDefault(i => i.Name == newOrganization.Name);

            Assert.That(organizationStore, Is.Not.Empty);
            Assert.That(organizationStore.Count, Is.EqualTo(2));
            Assert.That(itemAdded, Is.Not.Null);
            Assert.That(itemAdded.OrganizationRole, Is.Not.Empty);
            Assert.That(itemAdded.OrganizationRole[0].OrganizationType, Is.EqualTo(OrganizationType.ServiceProvider));
        }

        [Test]
        public void UpdatesServiceProviderInRepository()
        {
                var newOrganization = new Organization()
                                        {
                                            Id = 10,
                                            Name = "New Organization",
                                            PostalCode = "123456",
                                            Address = "Address1"
                                        };

            organizationService.SaveServiceProviderOrganization(newOrganization);

            Assert.That(organizationStore[0].Name, Is.EqualTo(newOrganization.Name));
        }

        [Test]
        public void ThrowsArgumentNullExceptionWhenNullServiceProviderOrganization()
        {
            Assert.Throws<ArgumentNullException>(() => organizationService.SaveServiceProviderOrganization(null));                
        }
    }

    [TestFixture]
    public class GetAllOrganizationsOfTypeWithParentIdMethod : OrganizationServiceTestsBase
    {
        private IList<OrganizationRole> organizationRoleList;

        private IOrganizationsService organizationService;

        private IList<OrganizationRole> filteredResults;

        [SetUp]
        public void TestSetup()
        {
            organizationRoleList = new List<OrganizationRole>()
                                        {
                                            new OrganizationRole() { OrganizationType = OrganizationType.EOSOwner, Organization = new Organization() { Id = 1 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent, Organization = new Organization() { Id = 2 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.PortalAgent, Organization = new Organization() { Id = 3 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider, ParentOrganizationId = 2, Organization = new Organization() { Id = 4 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider, ParentOrganizationId = 2, Organization = new Organization() { Id = 5 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.ServiceProvider, ParentOrganizationId = 3, Organization = new Organization() { Id = 6 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() { Id = 7 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() { Id = 8 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() { Id = 9 } },
                                            new OrganizationRole() { OrganizationType = OrganizationType.Customer, Organization = new Organization() { Id = 10 } },
                                        };                

            MockOrganizationRepository = new Mock<IRepository<Organization>>();
            MockOrganizationRoleRepository = new Mock<IRepository<OrganizationRole>>();
            MockOrganizationRoleUserRepository = new Mock<IRepository<OrganizationRoleUser>>();

            MockOrganizationRoleRepository.Setup(a => a.FindAll(It.IsAny<Expression<Func<OrganizationRole, bool>>>())).Callback( 
                (Expression<Func<OrganizationRole, bool>> pred) => filteredResults = organizationRoleList.AsQueryable().Where(pred).ToList()).Returns(() => filteredResults);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ReturnsEmptyListIfNoOrganizationsFound()
        {
            var result = organizationService.GetAllOrganizationsOfType(OrganizationType.ServiceProvider, 4);

            Assert.That(result, Is.Empty);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ReturnsFilteredListOfRequestedType()
        {
            var result = organizationService.GetAllOrganizationsOfType(OrganizationType.ServiceProvider, 2);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));                
        }
    }

    [TestFixture]
    public class GetServiceProviderOrganizationMethod : OrganizationServiceTestsBase
    {
        private IList<OrganizationRole> organizationList;

        private OrganizationRole foundOrganization = null;

        private IOrganizationsService organizationService;

        [SetUp]
        public void TestSetup()
        {
            organizationList = new List<OrganizationRole>()
                                        {
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.Customer,
                                                    Organization = new Organization()
                                                            {
                                                                Id = 1,
                                                                Name = "Customer Organization"
                                                            }
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.EOSOwner
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.PortalAgent,
                                                    Organization = new Organization()
                                                            {
                                                                Id = 3,
                                                                Name = "Portal Agent Organization"
                                                            }
                                                },
                                            new OrganizationRole()
                                                {
                                                    OrganizationType = OrganizationType.ServiceProvider,
                                                    Organization = new Organization()
                                                            {
                                                                Id = 4,
                                                                Name = "Portal Agent Organization"
                                                            }
                                                }
                                        };

            MockOrganizationRepository = new Mock<IRepository<Organization>>();
            MockOrganizationRoleRepository = new Mock<IRepository<OrganizationRole>>();
            MockOrganizationRoleUserRepository = new Mock<IRepository<OrganizationRoleUser>>();

            MockOrganizationRoleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<OrganizationRole, bool>>>()))
                .Callback(
                    (Expression<Func<OrganizationRole, bool>> pred) => foundOrganization = organizationList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundOrganization);

            organizationService = this.ServiceUnderTest();
        }

        [Test]
        public void ReturnsPortalAgentOrganizationIfFound()
        {
            const int ServiceProviderOrganizationIdToUse = 4;
                
            // ACT 
            var result = organizationService.GetServiceProviderOrganization(ServiceProviderOrganizationIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(4));

            MockOrganizationRoleRepository.Verify();
        }
    }

    [TestFixture]
    public class GetUsersMethod : OrganizationServiceTestsBase
    {
        [Test]
        public void ReturnsUsersForOrganizationTypeAndId()
        {
            var organizationalUsers = new List<OrganizationRoleUser>
                                          {
                                              new OrganizationRoleUser
                                                  {
                                                      Id = 1,
                                                      Role = new OrganizationRole() { OrganizationId = 1, OrganizationType = OrganizationType.ServiceProvider },
                                                      UserId = 1
                                                  },
                                              new OrganizationRoleUser()
                                                  {
                                                      Id = 2,
                                                      Role = new OrganizationRole() { OrganizationId = 2, OrganizationType = OrganizationType.ServiceProvider },
                                                      UserId = 2
                                                  },
                                              new OrganizationRoleUser()
                                                  {
                                                      Id = 3,
                                                      Role = new OrganizationRole() { OrganizationId = 1, OrganizationType = OrganizationType.Customer },
                                                      UserId = 3
                                                  }
                                          };

            MockOrganizationRoleUserRepository.Setup(
                r => r.FindAll(It.IsAny<Expression<Func<OrganizationRoleUser, bool>>>()))
                .Returns(
                    (Expression<Func<OrganizationRoleUser, bool>> predicate) => organizationalUsers.AsQueryable().Where(predicate).ToList());

            const int OrganizationIdToUse = 1;

            var result = ServiceUnderTest().GetUsers(OrganizationIdToUse, OrganizationType.ServiceProvider).ToList();

            const int UserIdFound = 1;

            Assert.That(result.Count(), Is.EqualTo(1));

            Assert.That(result[0].UserId, Is.EqualTo(UserIdFound));
        }

        [Test]
        public void ReturnsEmptyListIfNoneFound()
        {    
            var organizationalUsers = new List<OrganizationRoleUser>
                                          {
                                              new OrganizationRoleUser
                                                  {
                                                      Id = 1,
                                                      Role = new OrganizationRole() { OrganizationId = 1, OrganizationType = OrganizationType.ServiceProvider },
                                                      UserId = 1
                                                  },
                                              new OrganizationRoleUser()
                                                  {
                                                      Id = 2,
                                                      Role = new OrganizationRole() { OrganizationId = 2, OrganizationType = OrganizationType.ServiceProvider },
                                                      UserId = 2
                                                  },
                                              new OrganizationRoleUser()
                                                  {
                                                      Id = 3,
                                                      Role = new OrganizationRole() { OrganizationId = 1, OrganizationType = OrganizationType.Customer },
                                                      UserId = 3
                                                  }
                                          };

            MockOrganizationRoleUserRepository.Setup(
                r => r.FindAll(It.IsAny<Expression<Func<OrganizationRoleUser, bool>>>()))
                .Returns(
                    (Expression<Func<OrganizationRoleUser, bool>> predicate) => organizationalUsers.AsQueryable().Where(predicate).ToList());

            const int OrganizationIdToUse = 4;

            var result = ServiceUnderTest().GetUsers(OrganizationIdToUse, OrganizationType.ServiceProvider).ToList();

            Assert.That(result, Is.Empty);           
        }
    }

    [TestFixture]
    public class GetOrganizationMethod : OrganizationServiceTestsBase
    {
        [Test]
        public void ReturnsFoundOrganization()
        {
            var organizationList = new List<Organization>
                                       {
                                            new Organization { Id = 1, Name = "Organization 1" },
                                            new Organization { Id = 2, Name = "Organization 2" },
                                            new Organization { Id = 3, Name = "Organization 3" },
                                       };

            MockOrganizationRepository.Setup(
                r => r.Find(It.IsAny<Expression<Func<Organization, bool>>>()))
                .Returns(
                    (Expression<Func<Organization, bool>> predicate) =>
                        {
                            return organizationList.AsQueryable().SingleOrDefault(predicate);
                        });

            const int OrganizationIdToUse = 1;

            var result = ServiceUnderTest().GetOrganization(OrganizationIdToUse);

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Id, Is.EqualTo(OrganizationIdToUse));
        }

        [Test]
        public void ReturnsNullForNotFoundOrganization()
        {
            var organizationList = new List<Organization>
                                       {
                                            new Organization { Id = 1, Name = "Organization 1" },
                                            new Organization { Id = 2, Name = "Organization 2" },
                                            new Organization { Id = 3, Name = "Organization 3" },
                                       };

            MockOrganizationRepository.Setup(
                r => r.Find(It.IsAny<Expression<Func<Organization, bool>>>()))
                .Returns(
                    (Expression<Func<Organization, bool>> predicate) => organizationList.AsQueryable().SingleOrDefault(predicate));

            const int OrganizationIdToUse = 4;

            var result = ServiceUnderTest().GetOrganization(OrganizationIdToUse);

            Assert.That(result, Is.Null);
        }
    }
}