namespace EOS2.Web.BDD.Specs.Channels.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.BDD.Specs.Common;
    using EOS2.Web.BDD.Specs.SetUp;
    
    using Microsoft.Practices.Unity;

    using TechTalk.SpecFlow;
    
    [Binding]
    public class SharedSteps
    {
        private static readonly string StoryId = BeforeAfterTests.GetStoryId();

        [BeforeScenario("CreateChannelsOnAnInstrument")]
        public static void CreateChannelsOnAnInstrumentSetup()
        {
            DatabaseMaintenance.Reset();

            var user = OrganizationMaintenance.User("kim.roberts");

            var organizationService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();

            var usersOrganizationRoles = organizationService.GetUsersOrganizationalRoles(user.Id);

            var userOrganization = usersOrganizationRoles.First().Organization;

            SiteMaintenance.AddSite(
                userOrganization,
                new Site
                    {
                        Name = StoryId + "Site With Channels",
                        Address = "123 the street, somewhere close in someplace",
                        PostalCode = "BN13 1JK",
                        PlantAreas = new List<PlantArea>
                                        {
                                            new PlantArea
                                                {
                                                    Name = StoryId + "Plant Area with Channels",                                                    
                                                    Instruments = new List<Instrument>
                                                                      {
                                                                        new Instrument()
                                                                            {
                                                                                Name = StoryId + "Instrument 1",
                                                                                CalibrationFrequencyId = 1,
                                                                                TypeId = 1
                                                                            }                                                                          
                                                                      }
                                                }
                                        }
                    });
        }

        [BeforeScenario("CreateInstrumentChannelsAndEquipment")]
        public static void CreateInstrumentChannelsAndEquipmentSetup()
        {
            DatabaseMaintenance.Reset();

            var user = OrganizationMaintenance.User("kim.roberts");

            var organizationService = BeforeAfterTests.DependencyContainer.Resolve<IOrganizationsService>();

            var usersOrganizationRoles = organizationService.GetUsersOrganizationalRoles(user.Id);

            var userOrganization = usersOrganizationRoles.First().Organization;

            SiteMaintenance.AddSite(
                userOrganization,
                new Site
                    {
                        Name = StoryId + "Site With Channels",
                        Address = "123 the street, somewhere close in someplace",
                        PostalCode = "BN13 1JK",
                        PlantAreas = new List<PlantArea>
                                        {
                                            new PlantArea
                                                {
                                                    Name = StoryId + "Plant Area with Channels",                                                    
                                                    Instruments = new List<Instrument>
                                                                      {
                                                                        new Instrument()
                                                                            {
                                                                                Name = StoryId + "Instrument 1",
                                                                                CalibrationFrequencyId = 1,
                                                                                TypeId = 1,
                                                                                Channels = new List<Channel>
                                                                                               {
                                                                                                   new Channel
                                                                                                       {
                                                                                                           Name = StoryId + "0000 Instrument1",
                                                                                                           Number = "0000",
                                                                                                           ScheduleFrequencyId = 1,
                                                                                                           Type = new ChannelType { Id = 1 }
                                                                                                       },
                                                                                                   new Channel
                                                                                                       {
                                                                                                           Name = StoryId + "0001 Instrument1",
                                                                                                           Number = "0001",
                                                                                                           ScheduleFrequencyId = 1,
                                                                                                           Type = new ChannelType { Id = 1 }
                                                                                                       },
                                                                                                   new Channel
                                                                                                        {
                                                                                                           Name = StoryId + "0002 - Instrument1",
                                                                                                           Number = "0002",
                                                                                                           ScheduleFrequencyId = 1,
                                                                                                           Type = new ChannelType { Id = 1 }
                                                                                                       },
                                                                                                   new Channel
                                                                                                        {                                                                                                       
                                                                                                           Name = StoryId + "0003 - Instrument1",
                                                                                                           Number = "0003",
                                                                                                           ScheduleFrequencyId = 1,
                                                                                                           Type = new ChannelType { Id = 1 }
                                                                                                       },
                                                                                                   new Channel
                                                                                                        {                                                                                                       
                                                                                                           Name = StoryId + "0004 - Instrument1",
                                                                                                           Number = "0004",
                                                                                                           ScheduleFrequencyId = 1,
                                                                                                           Type = new ChannelType { Id = 1 }
                                                                                                       }
                                                                                               }
                                                                            }                                                                          
                                                                      },
                                                    Equipments = new List<Equipment>
                                                                     {
                                                                        new Equipment
                                                                            {
                                                                                Name = StoryId + "Equipment 1",
                                                                                TypeId = 1,
                                                                                Make  = "Test Equipment",
                                                                                Model = "Test Model",
                                                                                SerialNumber = "123456 TEST"
                                                                            },
                                                                        new Equipment
                                                                            {
                                                                                Name = StoryId + "Equipment 2",
                                                                                TypeId = 1,
                                                                                Make  = "Test Equipment",
                                                                                Model = "Test Model",
                                                                                SerialNumber = "123458 TEST"
                                                                            }                                 
                                                                     }
                                                }
                                        }
                    });         
        }
    }
}
