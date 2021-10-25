namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using EOS2.Model;
    using EOS2.Web.BDD.Specs.Common;
    using EOS2.Web.BDD.Specs.SetUp;

    using TechTalk.SpecFlow;

    [Binding]
    public class SharedSteps
    {
        private static readonly string StoryId = BeforeAfterTests.GetStoryId();

        [BeforeScenario("CreatePortalAgentOrganization")]
        public static void CreatePortalAgentOrganizationSetup()
        {
            DatabaseMaintenance.Reset();

            OrganizationMaintenance.AddPortalAgent(
                new Organization
                    {
                        Name = StoryId + "Duplicate Eurotherm EU",
                        Address = "123 the street, somewhere close in someplace",
                        PostalCode = "BN13 1JK"
                    });
        }

        [BeforeScenario("CreateUniqueOrganizationsRoles")]
        public static void CreateUniqueOrganizationsRolesSetup()
        {
            DatabaseMaintenance.Reset();

            var portalAgents = new[]
                                   {
                                       new Organization
                                           {
                                               Name = StoryId + "First BDD Portal Agent (not this one)",
                                               Address = "somewhere close in someplace",
                                               PostalCode = "SW12 1LN"
                                           },
                                       new Organization
                                           {
                                               Name = StoryId + "Eurotherm BDD Portal Agent EU",
                                               Address = "123 the street, somewhere close in someplace",
                                               PostalCode = "BN13 1JK"
                                           },
                                       new Organization
                                           {
                                               Name = StoryId + "Last BDD Portal Agent (not this one)",
                                               Address = "somewhere place in somewhere",
                                               PostalCode = "SP12 1PM"
                                           }
                                   };
            OrganizationMaintenance.AddPortalAgents(portalAgents);
            var serviceProviders = new[]
                                       {
                                           new Organization
                                               {
                                                   Name =
                                                       StoryId
                                                       + "First BDD Service Provider (not this one)",
                                                   Address = "somewhere close in someplace",
                                                   PostalCode = "SW12 1LN"
                                               },
                                           new Organization
                                               {
                                                   Name = StoryId + "Eurotherm BDD Service Provider EU",
                                                   Address = "123 the street, somewhere close in someplace",
                                                   PostalCode = "BN13 1JK"
                                               },
                                           new Organization
                                               {
                                                   Name =
                                                       StoryId + "Last BDD Service Provider (not this one)",
                                                   Address = "somewhere place in somewhere",
                                                   PostalCode = "SP12 1PM"
                                               }
                                       };
            OrganizationMaintenance.AddServiceProviders(serviceProviders);
            var customerOrganizations = new[]
                                            {
                                                new Organization
                                                    {
                                                        Name = StoryId + "First BDD Customer (not this one)",
                                                        Address = "somewhere close in someplace",
                                                        PostalCode = "SW12 1LN"
                                                    },
                                                new Organization
                                                    {
                                                        Name = StoryId + "Eurotherm BDD Customer EU",
                                                        Address =
                                                            "123 the street, somewhere close in someplace",
                                                        PostalCode = "BN13 1JK"
                                                    },
                                                new Organization
                                                    {
                                                        Name = StoryId + "Last BDD Customer (not this one)",
                                                        Address = "somewhere place in somewhere",
                                                        PostalCode = "SP12 1PM"
                                                    }
                                            };
            OrganizationMaintenance.AddCustomers(customerOrganizations);
        }

        [BeforeScenario("CreateServiceProviderOrganization")]
        public static void CreateServiceProviderOrganizationSetup()
        {
            DatabaseMaintenance.Reset();

            // we need the id of the logged in users Portal Agent Organization
            var user = OrganizationMaintenance.User("keith.kraylic");
            var parentOrganizationRole = OrganizationMaintenance.PortalAgentOrganizationRole(user);
            OrganizationMaintenance.AddServiceProvider(
                new Organization
                    {
                        Name = StoryId + "Duplicate Service Provider",
                        Address = "Duplicate Street, Duplicate Place",
                        PostalCode = "DP13 1JK"
                    },
                parentOrganizationRole.Organization.Id);
        }

        [BeforeScenario("OrganizationUsersCreateOrganizations")]
        public static void OrganizationUsersCreateOrganizationsSetup()
        {
            DatabaseMaintenance.Reset();
        }

        [BeforeScenario("EOSOwnerEditAnOrganization")]
        public static void EOSOwnerEditAnOrganizationSetup()
        {
            DatabaseMaintenance.Reset();

            CreateUniqueOrganizationsRolesSetup();
        }

        [BeforeScenario("OrganizationUserCreatesUserAccount")]
        public static void OrganizationUserCreatesUserAccountSetup()
        {
            DatabaseMaintenance.Reset();
        }

        [BeforeScenario("CreateOrganizationTreeForUserCreation")]
        public static void CreateOrganizationTreeForUserCreationSetup()
        {
            DatabaseMaintenance.Reset();

            var user = OrganizationMaintenance.User("keith.kraylic");
            var parentOrganizationRole = OrganizationMaintenance.PortalAgentOrganizationRole(user);
            var portalAgent =
                OrganizationMaintenance.AddPortalAgent(
                    new Organization
                        {
                            Name = StoryId + "New Portal Agent Organization",
                            Address = "somewhere close in someplace",
                            PostalCode = "SW12 1LN"
                        });
            OrganizationMaintenance.AddServiceProvider(
                new Organization
                    {
                        Name = StoryId + "New Service Provider Organization",
                        Address = "somewhere close in someplace",
                        PostalCode = "SW12 1LN"
                    },
                portalAgent.Id);
            OrganizationMaintenance.AddServiceProvider(
                new Organization
                    {
                        Name = StoryId + "New Portal Agent Service Provider Organization",
                        Address = "somewhere close in someplace",
                        PostalCode = "SW12 1LN"
                    },
                parentOrganizationRole.Organization.Id);
            OrganizationMaintenance.AddCustomer(
                new Organization
                    {
                        Name = StoryId + "New Customer Organization",
                        Address = "somewhere close in someplace",
                        PostalCode = "SW12 1LN"
                    });
        }
    }
}
