namespace EOS2.Data.Migrations.DataSeeding
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using EOS2.Identity.Model;
    using EOS2.Identity.Repository;
    using EOS2.Identity.Services;
    using EOS2.Model;
    using EOS2.Model.Enums;

    using Microsoft.AspNet.Identity;

    internal static class DataSeeding
    {
        internal static ConnectionStringSettings ConnectionStringInfo { get; set; }

        internal static TimeSpan TotalSeedTime { get; set; }        

        internal static void SeedSecurityData()
        {
            var startTime = DateTime.Now;

            using (var securityContext = new Contexts.SecurityDbContext(ConnectionStringInfo.ConnectionString))
            {
                AddSecurityRoles(securityContext);

                AddUserAccounts(securityContext);
                
                securityContext.SaveChanges();

                SetUserPasswords(securityContext);
            }

            var totalElpasedTime = DateTime.Now - startTime;
            TotalSeedTime += totalElpasedTime;
            Console.WriteLine("Seed Security Elapsed Time : {0}:{1}:{2}", totalElpasedTime.Hours, totalElpasedTime.Minutes, totalElpasedTime.Seconds);
        }

        /// <summary>
        /// Security Roles in context of EOS2 are just if someone has access to the system
        /// EOS2 uses claims which is information about the person and not some contrived role they have
        /// 
        /// Roles are basic and consist of 
        ///     Admin - someone that can manage EOS2
        ///     User - someone that is allowed to use EOS2
        ///     APIUSer - someone that can access EOS2 via API
        /// </summary>
        /// <param name="context"></param>
        internal static void AddSecurityRoles(Contexts.SecurityDbContext context)
        {
            if (!context.Roles.Any(u => u.Name == "Admin"))
            {
                context.Roles.AddOrUpdate(new Role() { Name = "Admin" });
            }

            if (!context.Roles.Any(u => u.Name == "User"))
            {
                context.Roles.AddOrUpdate(new Role() { Name = "User" });
            }

            if (!context.Roles.Any(u => u.Name == "APIUser"))
            {
                context.Roles.AddOrUpdate(new Role() { Name = "APIUser" });
            }
        }

        internal static void AddUserAccounts(Contexts.SecurityDbContext context)
        {
            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "EOS",
                        FamilyName = "Owner",
                        Email = "admin@EOSOwner.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "EOS.Owner.Admin"
                    });

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Portal",
                        FamilyName = "Agent",
                        Email = "admin@EOSPortal.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "EOS.Portal.Admin"
                    });

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Steve",
                        FamilyName = "Sprott",
                        Email = "steve.sprott@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "steve.sprott"
                    });

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Daniel",
                        FamilyName = "Priory",
                        Email = "daniel.priory@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "daniel.priory"
                    });

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Keith",
                        FamilyName = "Kraylic",
                        Email = "keith.kraylic@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "keith.kraylic"
                    });

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Sam",
                        FamilyName = "Morris",
                        Email = "sam.morris@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "sam.morris"
                    });            

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Kim",
                        FamilyName = "Roberts",
                        Email = "kim.roberts@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "kim.roberts"
                    });           

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Damian",
                        FamilyName = "Brett",
                        Email = "damian.brett@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "damian.brett"
                    });     

            context.Users.AddOrUpdate(
                    new User()
                    {
                        Name = "Scott",
                        FamilyName = "Roberts",
                        MiddleName = "S",
                        Email = "scott.roberts@test.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        PhoneNumber = "987654321",
                        UserName = "scott.roberts"
                    });           
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Only need to catch and report")]
        internal static void SetUserPasswords(Contexts.SecurityDbContext context)
        {
            try
            {
                using (var userRepository = new UserRepository(context))
                {
                    using (var userService = new IdentityUserService(userRepository))
                    {
                        var userId = context.Users.Single(u => u.UserName == "EOS.Owner.Admin").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");
                        userService.AddToRole(userId, "Admin");

                        userId = context.Users.Single(u => u.UserName == "EOS.Portal.Admin").Id;
                        userService.AddPassword(userId, "!12345678A");                        
                        userService.AddToRole(userId, "User");
                        userService.AddToRole(userId, "Admin");

                        userId = context.Users.Single(u => u.UserName == "steve.sprott").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");
                        userService.AddToRole(userId, "Admin");

                        userId = context.Users.Single(u => u.UserName == "daniel.priory").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");

                        userId = context.Users.Single(u => u.UserName == "keith.kraylic").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");

                        userId = context.Users.Single(u => u.UserName == "sam.morris").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");

                        userId = context.Users.Single(u => u.UserName == "kim.roberts").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");

                        userId = context.Users.Single(u => u.UserName == "damian.brett").Id;
                        userService.AddPassword(userId, "!12345678A");
                        userService.AddToRole(userId, "User");

                        userId = context.Users.Single(u => u.UserName == "scott.roberts").Id;
                        userService.AddToRole(userId, "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void EOS2Data()
        {
            var startTime = DateTime.Now;

            using (var eos2DbContext = new Contexts.EOS2DbContext(ConnectionStringInfo.ConnectionString))
            {
                CreateMultiroleOrganization(eos2DbContext);

                CreatePortalAgentOrganization(eos2DbContext);

                CreateServiceProviderOrganization(eos2DbContext);

                CreateCustomerOrganization(eos2DbContext);
            }

            var totalElpasedTime = DateTime.Now - startTime;
            TotalSeedTime += totalElpasedTime;
            Console.WriteLine("Seed EOS2Data Elapsed Time : {0}:{1}:{2}", totalElpasedTime.Hours, totalElpasedTime.Minutes, totalElpasedTime.Seconds);
        }

        internal static void ReferenceData()
        {
            var channelTypes = new[]
                                   {
                                       new ChannelType() { Name = "Analogue" }
                                   };

            using (var eos2DbContext = new Contexts.EOS2DbContext(ConnectionStringInfo.ConnectionString))
            {
                eos2DbContext.ChannelTypes.AddRange(channelTypes);

                eos2DbContext.SaveChanges();
            }
        }

        private static void CreateCustomerOrganization(Contexts.EOS2DbContext eos2DbContext)
        {
                var newEosCustomer = new Organization()
                                      {
                                          Name = "Eurotherm Customer",
                                          Address = "Faraday Close, Worthing, West Sussex",
                                          PostalCode = "BN13 3PL",
                                          OrganizationRole =
                                              new[]
                                                  {
                                                      new OrganizationRole()
                                                          {
                                                              Comment = "Customer Organization Type",
                                                              FromDate = DateTime.Now,
                                                              Name = "Eurotherm Customer",
                                                              OrganizationType = OrganizationType.Customer
                                                          }
                                                  }
                                      };

                eos2DbContext.Organizations.Add(newEosCustomer);

                eos2DbContext.SaveChanges();

            using (var securityContext = new Contexts.SecurityDbContext(ConnectionStringInfo.ConnectionString))
            {
                var customerUser = securityContext.Users.Single(u => u.UserName == "kim.roberts");

                eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                        {
                            Role = newEosCustomer.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.Customer),
                            UserId = customerUser.Id
                        });

                customerUser = securityContext.Users.Single(u => u.UserName == "damian.brett");

                eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                        {
                            Role = newEosCustomer.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.Customer),
                            UserId = customerUser.Id
                        });

                eos2DbContext.SaveChanges();
            }
        }

        private static void CreateServiceProviderOrganization(Contexts.EOS2DbContext eos2DbContext)
        {
            var portalAgent = eos2DbContext.Organizations.SingleOrDefault(or => or.Name == "Portal Agent Organization");

            var newServiceProvider = new Organization()
                                      {
                                          Name = "Service Provider Organization",
                                          Address = "Service Provider, Worthing, West Sussex",
                                          PostalCode = "BN13 3PL",
                                          OrganizationRole =
                                              new[]
                                                  {
                                                      new OrganizationRole()
                                                          {
                                                              Comment = "Service Provider Organization Type",
                                                              FromDate = DateTime.Now,
                                                              Name = "Service Provider ",
                                                              OrganizationType = OrganizationType.ServiceProvider,
                                                              ParentOrganizationId = portalAgent.Id
                                                          }
                                                  }
                                      };

            eos2DbContext.Organizations.Add(newServiceProvider);
            eos2DbContext.SaveChanges();

            using (var securityContext = new Contexts.SecurityDbContext(ConnectionStringInfo.ConnectionString))
            {
                var serviceProviderUser = securityContext.Users.Single(u => u.UserName == "sam.morris");

                eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                        {
                            Role = newServiceProvider.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.ServiceProvider),
                            UserId = serviceProviderUser.Id
                        });

                eos2DbContext.SaveChanges();
            }
        }

        private static void CreatePortalAgentOrganization(Contexts.EOS2DbContext eos2DbContext)
        {
                var newEosPortalAgent = new Organization()
                                      {
                                          Name = "Portal Agent Organization",
                                          Address = "Portal Agent, Worthing, West Sussex",
                                          PostalCode = "BN13 3PL",
                                          OrganizationRole =
                                              new[]
                                                  {
                                                      new OrganizationRole()
                                                          {
                                                              Comment = "Portal Agent Organization Type",
                                                              FromDate = DateTime.Now,
                                                              Name = "Portal Agent ",
                                                              OrganizationType = OrganizationType.PortalAgent
                                                          }
                                                  }
                                      };

                eos2DbContext.Organizations.Add(newEosPortalAgent);
                eos2DbContext.SaveChanges();

            using (var securityContext = new Contexts.SecurityDbContext(ConnectionStringInfo.ConnectionString))
            {
                var portalAgentUser = securityContext.Users.Single(u => u.UserName == "keith.kraylic");

                eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                        {
                            Role = newEosPortalAgent.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.PortalAgent),
                            UserId = portalAgentUser.Id
                        });

                eos2DbContext.SaveChanges();
            }
        }

        private static void CreateMultiroleOrganization(Contexts.EOS2DbContext eos2DbContext)
        {
                var newEosOwner = new Organization()
                                      {
                                          Name = "Eurotherm ",
                                          Address = "Faraday Close, Worthing, West Sussex",
                                          PostalCode = "BN13 3PL",
                                          OrganizationRole = new List<OrganizationRole>()
                                                  {
                                                      new OrganizationRole()
                                                          {
                                                              Comment = "EOS Owner Organization Type",
                                                              FromDate = DateTime.Now,
                                                              Name = "EOS Owner - Eurotherm Ltd",
                                                              OrganizationType = OrganizationType.EOSOwner
                                                          },
                                                      new OrganizationRole()
                                                          {
                                                              Comment = "EOS Portal Agent Organization Type",
                                                              FromDate = DateTime.Now,
                                                              Name = "Portal Agent - Eurotherm Ltd",
                                                              OrganizationType = OrganizationType.PortalAgent
                                                          }
                                                  }
                                      };
                eos2DbContext.Organizations.Add(newEosOwner);
                eos2DbContext.SaveChanges();

            var serviceProvderRole = new OrganizationRole()
                                         {
                                             OrganizationId = newEosOwner.Id,
                                             Comment = "EOS Service Provider Organization Type",
                                             FromDate = DateTime.Now,
                                             Name = "Service Provider - Eurotherm Ltd",
                                             OrganizationType = OrganizationType.ServiceProvider,
                                             ParentOrganizationId = newEosOwner.Id
                                         };

            eos2DbContext.OrganizationRoles.Add(serviceProvderRole);
            eos2DbContext.SaveChanges();

                using (var securityContext = new Contexts.SecurityDbContext(ConnectionStringInfo.ConnectionString))
                {
                    var eosOwnerDefaultUser = securityContext.Users.Single(u => u.UserName == "EOS.Owner.Admin");

                    eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                            {
                                Role = newEosOwner.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.EOSOwner),
                                UserId = eosOwnerDefaultUser.Id
                            });

                    var eosOwnerUser = securityContext.Users.Single(u => u.UserName == "steve.sprott");

                    eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                            {
                                Role = newEosOwner.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.EOSOwner),
                                UserId = eosOwnerUser.Id
                            });

                    eosOwnerUser = securityContext.Users.Single(u => u.UserName == "scott.roberts");

                    eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                            {
                                Role = newEosOwner.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.EOSOwner),
                                UserId = eosOwnerUser.Id
                            });

                    var portalAgentUser = securityContext.Users.Single(u => u.UserName == "EOS.Portal.Admin");
                    eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                            {
                                Role = newEosOwner.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.PortalAgent),
                                UserId = portalAgentUser.Id
                            });

                    portalAgentUser = securityContext.Users.Single(u => u.UserName == "daniel.priory");
                    eos2DbContext.OrganizationRoleUsers.Add(new OrganizationRoleUser()
                            {
                                Role = newEosOwner.OrganizationRole.Single(r => r.OrganizationType == OrganizationType.PortalAgent),
                                UserId = portalAgentUser.Id
                            });

                    eos2DbContext.SaveChanges();
                }
        }
    }
}
