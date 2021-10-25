namespace EOS2.Web.BDD.Specs.ServiceProvider.Steps
{
    using System;
    using System.Globalization;
    using System.Linq;

    using EOS2.Model;

    using EOS2.Web.BDD.Specs.Common;
    using EOS2.Web.BDD.Specs.SetUp;

    using TechTalk.SpecFlow;

    [Binding]
    public class SharedSteps
    {
        private static readonly string StoryId = GetStoryId();

        [BeforeScenario("CreateCustomerToPlantAreaEquipment")]
        public static void CreateCustomerToPlantAreaEquipmentSetup()
        {
            DatabaseMaintenance.Reset();

            var equipmentType = SiteMaintenance.EquipmentType("HIP Furnace");

            var organization =
                OrganizationMaintenance.AddCustomer(
                    new Organization
                        {
                            Name = StoryId + "Eurotherm EU Customer",
                            Address = "An Address Someplace",
                            PostalCode = "A PostCode"
                        });
            var organizationSite = SiteMaintenance.AddSite(
                organization,
                new Site
                    {
                        Name = StoryId + "Master Furnace",
                        Address = "An Address Someplace",
                        PostalCode = "A PostCode"
                    });
            var sitePlantArea = SiteMaintenance.AddPlantArea(
                organizationSite,
                new PlantArea { Name = StoryId + "Main Production Area", Description = "Plant Main Production Area" });
            var equipments = new[]
                                 {
                    new Equipment
                        {
                            Name = StoryId + "BDD Duplicate Tester",
                            Make = "Duplicate Tester",
                            Model = "Duplicate Tester",
                            SerialNumber = "123456",
                            TypeId = equipmentType.Id
                        },
                    new Equipment
                        {
                            Name = StoryId + "BDD Editable Tester",
                            Make = "Editable Tester",
                            Model = "Editable Tester",
                            SerialNumber = "123456",
                            TypeId = equipmentType.Id
                        }
                };
            SiteMaintenance.CreateEquipment(sitePlantArea, equipments.ToArray());
        }

        [BeforeScenario("SingleCustomer")]
        public static Organization SingleCustomerSetup()
        {
            // Call cleardown
            DatabaseMaintenance.Reset();

            return
                OrganizationMaintenance.AddCustomer(
                    new Organization
                        {
                            Name = StoryId + "-Test Customer 1",
                            Address = "Dummy Address",
                            PostalCode = "BN13 3PL"
                        });
        }

        [BeforeScenario("SingleSite")]
        public static Site SingleSiteSetup()
        {
            // A Site needs a Customer
            var customer = SingleCustomerSetup();
            return SiteMaintenance.AddSite(
                customer,
                new Site { Name = StoryId + "-Test Site 1", Address = "Dummy Address", PostalCode = "BN13 3PL" });
        }

        [BeforeScenario("SinglePlantArea")]
        public static PlantArea SinglePlantAreaSetup()
        {
            // A Plant Area needs a Site
            var site = SingleSiteSetup();
            return SiteMaintenance.AddPlantArea(site, new PlantArea { Name = StoryId + "-Plant Area 1", Description = "Dummy Description" });
        }

        [BeforeScenario("SingleInstrument")]
        public static Instrument SingleInstrumentSetup()
        {
            // An Instrument needs a Plant Area
            var plantArea = SinglePlantAreaSetup();
            return CreateSingleInstrument(plantArea);
        }

        [BeforeScenario("SingleEquipment")]
        public static Equipment SingleEquipmentSetup()
        {
            // An Equipment needs a Plant Area
            var plantArea = SinglePlantAreaSetup();
            return CreateSingleEquipment(plantArea);
        }

        [BeforeScenario("SingleSchedule")]
        public static Schedule SingleScheduleSetup()
        {
            // A Schedule needs an Equipment
            var equipment = SingleEquipmentSetup();
            return SiteMaintenance.CreateSchedule(equipment, new Schedule { Name = StoryId + "-Schedule 1", FurnaceClassId = 1, TypeId = 1, FrequencyId = 1 });
        }

        [BeforeScenario("SingleCalibrationCertificate")]
        public static CertificateHeader SingleCalibrationCertificateSetup()
        {
            // A Calibration Certificate needs an Instrument
            var instrument = SingleInstrumentSetup();
            return SiteMaintenance.CreateCertificate(
                instrument,
                new CertificateHeader
                    {
                        CertificateNumber = StoryId + "-Test Certificate 1", 
                                                                         StartDate = Convert.ToDateTime("01/01/2014", CultureInfo.InvariantCulture), 
                                                                         EndDate = Convert.ToDateTime("01/12/2014", CultureInfo.InvariantCulture),
                                                                         TypeId = 1, 
                                                                         InstrumentId = instrument.Id,
                                                                         CertificateBody = new CertificateBody()
                                                                     });
        }

        [BeforeScenario("MultipleCustomers")]
        public static void MultipleCustomersSetup()
        {
            DatabaseMaintenance.Reset();

            var newCustomers = new[]
                {
                    new Organization { Name = StoryId + "-Test Customer 1", Address = "Dummy Address", PostalCode = "BN13 3PL" },
                    new Organization { Name = StoryId + "-Test Customer 2", Address = "Dummy Address", PostalCode = "BN13 3PL" },
                    new Organization { Name = StoryId + "-Test Customer 3", Address = "Dummy Address", PostalCode = "BN13 3PL" }
                };
            OrganizationMaintenance.AddCustomers(newCustomers.ToArray());
        }

        [BeforeScenario("MultipleSitesSingleCustomer")]
        public static Site[] MultipleSitesSingleCustomerSetup()
        {
            // the Sites needs a Customer
            var customer = SingleCustomerSetup();

            var newSites = new[]
                {
                    new Site
                        {
                            Name = StoryId + "-Test Site 1",
                            Address = "Dummy Address",
                            PostalCode = "BN13 3PL",
                        },
                    new Site
                        {
                            Name = StoryId + "-Test Site 2",
                            Address = "Dummy Address",
                            PostalCode = "BN13 3PL",
                        },
                    new Site
                        {
                            Name = StoryId + "-Test Site 3",
                            Address = "Dummy Address",
                            PostalCode = "BN13 3PL",
                        }
                };
            return SiteMaintenance.AddSites(customer, newSites.ToArray());
        }

        [BeforeScenario("MultiplePlantArea")]
        public static void MultiplePlantAreaSetup()
        {
            // We need a Site
            var site = SingleSiteSetup();

            var newPlantAreas = new[]
                {
                    new PlantArea { Name = StoryId + "-Test Plant Area 1", Description = "Dummy Description" },
                    new PlantArea { Name = StoryId + "-Test Plant Area 2", Description = "Dummy Description" },
                    new PlantArea { Name = StoryId + "-Test Plant Area 3", Description = "Dummy Description" }
                };

            SiteMaintenance.CreatePlantAreas(site, newPlantAreas.ToArray());
        }

        [BeforeScenario("MultipleInstrument")]
        public static void MultipleInstrumentSetup()
        {
            // We need a Plant Area
            var plantArea = SinglePlantAreaSetup();

            CreateMultipleInstrument(plantArea);
        }

        [BeforeScenario("MultipleSchedule")]
        public static void MultipleScheduleSetup()
        {
            // We need an Equipment
            var equipment = SingleEquipmentSetup();

            var schedules = new[]
                {
                    new Schedule { Name = StoryId + "-Test Schedule 1", FurnaceClassId = 1, FrequencyId = 1, TypeId = 1, EquipmentId = equipment.Id },
                    new Schedule { Name = StoryId + "-Test Schedule 2", FurnaceClassId = 2, FrequencyId = 1, TypeId = 1, EquipmentId = equipment.Id },
                    new Schedule { Name = StoryId + "-Test Schedule 3", FurnaceClassId = 3, FrequencyId = 1, TypeId = 1, EquipmentId = equipment.Id }
                };
            SiteMaintenance.CreateSchedules(equipment, schedules.ToArray());
        }

        [BeforeScenario("MultipleCalibrationCertificate")]
        public static void MultipleCalibrationCertificateSetup()
        {
            // We need an Instrument
            var instrument = SingleInstrumentSetup();

            var certificates = new[]
                                   {
                                       new CertificateHeader
                                           {
                                               CertificateNumber = StoryId + "-Test Certificate 1",
                                               StartDate = Convert.ToDateTime("01/01/2014", CultureInfo.InvariantCulture),
                                               EndDate = Convert.ToDateTime("01/12/2014", CultureInfo.InvariantCulture),
                                               TypeId = 1,
                                               InstrumentId = instrument.Id,
                                               CertificateBody = new CertificateBody()
                                           },
                                       new CertificateHeader
                                           {
                                               CertificateNumber = StoryId + "-Test Certificate 2",
                                               StartDate = Convert.ToDateTime("01/01/2014", CultureInfo.InvariantCulture),
                                               EndDate = Convert.ToDateTime("01/12/2014", CultureInfo.InvariantCulture),
                                               TypeId = 1,
                                               InstrumentId = instrument.Id,
                                               CertificateBody = new CertificateBody()
                                           }
                                   };
            SiteMaintenance.CreateCertificates(instrument, certificates.ToArray());
        }

        [BeforeScenario("SingleInstrumentSingleEquipment")]
        public static void SingleInstrumentSingleEquipmentSetup()
        {
            // An Instrument needs a Plant Area
            var plantArea = SinglePlantAreaSetup();

            CreateSingleInstrument(plantArea);

            CreateSingleEquipment(plantArea);
        }

        [BeforeScenario("SingleInstrumentMultipleEquipment")]
        public static void SingleInstrumentMultipleEquipmentSetup()
        {
            // An Instrument needs a Plant Area
            var plantArea = SinglePlantAreaSetup();

            CreateSingleInstrument(plantArea);

            CreateMultipleEquipment(plantArea);
        }

        [BeforeScenario("SingleInstrumentMultipleEquipmentSingleAttached")]
        public static void SingleInstrumentMultipleEquipmentSingleAttachedSetup()
        {
            // An Instrument needs a Plant Area
            var plantArea = SinglePlantAreaSetup();

            var instrument = CreateSingleInstrument(plantArea);

            var equipments = CreateMultipleEquipment(plantArea);

            // Attach them together
            foreach (var equipment in equipments)
            {
                SiteMaintenance.AttachInstrumentToEquipment(instrument, equipment);
            }
        }

        private static string GetStoryId()
        {
            var featureTitle = FeatureContext.Current.FeatureInfo.Title;

            return featureTitle.Substring(0, 3).ToString();
        }

        private static Instrument CreateSingleInstrument(PlantArea plantArea)
        {
            return SiteMaintenance.CreateInstrument(
                plantArea,
                new Instrument
                    {
                        Name = StoryId + "-Instrument 1",
                        Description = "Test Description 1",
                        Make = "Test Make 1",
                        Model = "Test Model 1",
                        SerialNumber = "I1",
                        TypeId = 1,
                        CalibrationFrequencyId = 1,
////                        ChannelCount = 1,
                        Notes = "Test Notes 1",
                    });
        }

        private static Equipment CreateSingleEquipment(PlantArea plantArea)
        {
            var equipment = new Equipment
                                {
                                    Name = StoryId + "-Equipment 1",
                                    TypeId = 1,
                                    Model = "Autoclave",
                                    SerialNumber = "E1"
                                };
            return SiteMaintenance.CreateEquipment(plantArea, equipment);
        }

        private static Equipment[] CreateMultipleEquipment(PlantArea plantArea)
        {
            var equipments = new[]
                {
                    new Equipment { Name = StoryId + "-Equipment 1", TypeId = 1, Model = "Autoclave", SerialNumber = "E1" },
                    new Equipment { Name = StoryId + "-Equipment 2", TypeId = 1, Model = "Autoclave", SerialNumber = "E2" },
                    new Equipment { Name = StoryId + "-Equipment 3", TypeId = 1, Model = "Autoclave", SerialNumber = "E3" }
                };
            return SiteMaintenance.CreateEquipment(plantArea, equipments.ToArray());
        }

        private static void CreateMultipleInstrument(PlantArea plantArea)
        {
            var newInstruments = new[]
                {
                    new Instrument
                        {
                            Name = StoryId + "-Instrument 1",
                            Description = "First instrument",
                            TypeId = 1,
                            Make = "Eurotherm",
                            Model = "Test Instrument 1",
                            SerialNumber = "I1",
                            CalibrationFrequencyId = 1
                        },
                    new Instrument
                        {
                            Name = StoryId + "-Instrument 2",
                            Description = "Second instrument",
                            TypeId = 2,
                            Make = "Eurotherm",
                            Model = "Test Instrument 2",
                            SerialNumber = "I2",
                            CalibrationFrequencyId = 2
                        },
                    new Instrument
                        {
                            Name = StoryId + "-Instrument 3",
                            Description = "Third instrument",
                            TypeId = 3,
                            Make = "Eurotherm",
                            Model = "Test Instrument 3",
                            SerialNumber = "I3",
                            CalibrationFrequencyId = 3
                        }
                };
            SiteMaintenance.CreateInstruments(plantArea, newInstruments.ToArray());
        }
    }
}
