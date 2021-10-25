namespace EOS2.Web.BDD.Specs.Common
{
    using System;
    using System.Linq;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.BDD.Specs.SetUp;

    using Microsoft.Practices.Unity;

    public class SiteMaintenance
    {
        public static Site AddSite(Organization organization, Site site)
        {
            if (organization == null) throw new ArgumentNullException("organization");
            if (site == null) throw new ArgumentNullException("site");

            site.OrganizationId = organization.Id;
            var siteService = BeforeAfterTests.DependencyContainer.Resolve<ISiteService>();
            if (siteService != null)
            {
                AddSite(siteService, organization, site);
            }

            return site;
        }

        public static Site[] AddSites(Organization organization, Site[] sites)
        {
            if (organization == null) throw new ArgumentNullException("organization");
            if (sites == null) throw new ArgumentNullException("sites");

            var siteService = BeforeAfterTests.DependencyContainer.Resolve<ISiteService>();
            if (siteService != null)
            {
                foreach (var site in sites)
                {
                    AddSite(siteService, organization, site);
                }
            }

            return sites;
        }

        private static void AddSite(ISiteService siteService, Organization organization, Site site)
        {
            site.OrganizationId = organization.Id;
            siteService.Save(site);
        }

        public static PlantArea AddPlantArea(Site site, PlantArea plantArea)
        {
            if (site == null) throw new ArgumentNullException("site");
            if (plantArea == null) throw new ArgumentNullException("plantArea");

            var plantAreaService = BeforeAfterTests.DependencyContainer.Resolve<IPlantAreaService>();
            if (plantAreaService != null)
            {
                plantArea.SiteId = site.Id;
                plantAreaService.SavePlantArea(plantArea);
            }

            return plantArea;
        }

        public static PlantArea[] CreatePlantAreas(Site site, PlantArea[] plantAreas)
        {
            if (site == null) throw new ArgumentNullException("site");
            if (plantAreas == null) throw new ArgumentNullException("plantAreas");

            var plantAreaService = BeforeAfterTests.DependencyContainer.Resolve<IPlantAreaService>();
            if (plantAreaService != null)
            {
                foreach (var plantArea in plantAreas)
                {
                    CreatePlantArea(plantAreaService, site, plantArea);
                }
            }

            return plantAreas;
        }

        public static Instrument CreateInstrument(PlantArea plantArea, Instrument instrument)
        {
            var instrumentService = BeforeAfterTests.DependencyContainer.Resolve<IInstrumentService>();
            if (instrumentService != null)
            {
                instrument = CreateInstrument(
                    instrumentService,
                    plantArea,
                    instrument);
            }

            return instrument;
        }

        public static Instrument[] CreateInstruments(PlantArea plantArea, Instrument[] instruments)
        {
            if (plantArea == null) throw new ArgumentNullException("plantArea");
            if (instruments == null) throw new ArgumentNullException("instruments");

            var instrumentService = BeforeAfterTests.DependencyContainer.Resolve<IInstrumentService>();
            if (instrumentService != null)
            {
                foreach (var instrument in instruments)
                {
                    CreateInstrument(instrumentService, plantArea, instrument);
                }
            }

            return instruments;
        }

        public static Schedule CreateSchedule(Equipment equipment, Schedule schedule)
        {
            if (equipment == null) throw new ArgumentNullException("equipment");

            var scheduleService = BeforeAfterTests.DependencyContainer.Resolve<IScheduleService>();
            if (scheduleService != null)
            {
                schedule = CreateSchedule(scheduleService, equipment, schedule);
            }

            return schedule;
        }

        public static Schedule[] CreateSchedules(Equipment equipment, Schedule[] schedules)
        {
            if (equipment == null) throw new ArgumentNullException("equipment");
            if (schedules == null) throw new ArgumentNullException("schedules");

            var scheduleService = BeforeAfterTests.DependencyContainer.Resolve<IScheduleService>();
            if (scheduleService != null)
            {
                foreach (var schedule in schedules)
                {
                    CreateSchedule(scheduleService, equipment, schedule);
                }
            }

            return schedules;
        }

        public static CertificateHeader[] CreateCertificates(Instrument instrument, CertificateHeader[] certificates)
        {
            if (instrument == null) throw new ArgumentNullException("instrument");
            if (certificates == null) throw new ArgumentNullException("certificates");

            var certificateService = BeforeAfterTests.DependencyContainer.Resolve<ICertificateService>();
            if (certificateService != null)
            {
                foreach (var certificate in certificates)
                {
                    CreateCertificate(certificateService, instrument, certificate);
                }
            }

            return certificates;
        }

        public static CertificateHeader CreateCertificate(Instrument instrument, CertificateHeader certificate)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            var certificateService = BeforeAfterTests.DependencyContainer.Resolve<ICertificateService>();
            if (certificateService != null)
            {
                certificate = CreateCertificate(certificateService, instrument, certificate);
            }

            return certificate;
        }

        public static void AttachInstrumentToEquipment(Instrument instrument, Equipment equipment)
        {
            if (instrument == null) throw new ArgumentNullException("instrument");
            if (equipment == null) throw new ArgumentNullException("equipment");
            ////var instrumentService = BeforeAfterTests.DependencyContainer.Resolve<IInstrumentService>();
            ////instrumentService.AttachEquipment(instrument.Id, equipment.Id);
        }

        public static EquipmentType EquipmentType(string typeName)
        {
            var equipmentTypeRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<EquipmentType>>();
            var equipmentType = equipmentTypeRepository.Find(et => et.Name == typeName);
            return equipmentType;
        }

        public static Equipment CreateEquipment(PlantArea plantArea, Equipment equipment)
        {
            var equipmentService = BeforeAfterTests.DependencyContainer.Resolve<IEquipmentService>();
            return CreateEquipment(equipmentService, plantArea, equipment);
        }

        public static Equipment[] CreateEquipment(PlantArea plantArea, Equipment[] equipments)
        {
            if (plantArea == null) throw new ArgumentNullException("plantArea");
            if (equipments == null) throw new ArgumentNullException("equipments");

            var equipmentService = BeforeAfterTests.DependencyContainer.Resolve<IEquipmentService>();
            if (equipmentService != null)
            {
                foreach (var equipment in equipments)
                {
                    CreateEquipment(equipmentService, plantArea, equipment);
                }
            }

            return equipments;
        }

        public static void ClearDown(string startText)
        {
            // Be aware that this only copes with the case where one matching instrument exists.
            var instrumentRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Instrument>>();
            var equipmentRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Equipment>>();
            var scheduleRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Schedule>>();
            var plantAreaRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<PlantArea>>();
            var siteRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Site>>();
            var certificateRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<CertificateHeader>>();
            var channelsRepository = BeforeAfterTests.DependencyContainer.Resolve<IRepository<Channel>>();
            //// This will be needed: var instrumentService = BeforeAfterTests.DatabaseContainer.Resolve<IInstrumentService>();

            certificateRepository.FindAll(c => c.CertificateNumber.StartsWith(startText))
                .ToList()
                .ForEach(
                    (certificate) => certificateRepository.Remove(certificate));

            scheduleRepository.FindAll(o => o.Name.StartsWith(startText))
                .ToList()
                .ForEach(
                    (schedule) => scheduleRepository.Remove(schedule));
       
            channelsRepository.FindAll(o => o.Name.StartsWith(startText))
                .ToList()
                .ForEach(
                    (channel) => channelsRepository.Remove(channel));

            instrumentRepository.FindAll(o => o.Name.StartsWith(startText))
                .ToList()
                .ForEach(
                    (instrument) => instrumentRepository.Remove(instrument));

            equipmentRepository.FindAll(i => i.Name.StartsWith(startText))
                .ToList()
                .ForEach(
                    (equipment) => equipmentRepository.Remove(equipment));           

            plantAreaRepository.FindAll(o => o.Name.StartsWith(startText))
                .ToList()
                .ForEach(
                    (plantArea) => plantAreaRepository.Remove(plantArea));

            siteRepository.FindAll(o => o.Name.StartsWith(startText))
                .ToList()
                .ForEach(
                    (site) => siteRepository.Remove(site));           
         }

        private static PlantArea CreatePlantArea(IPlantAreaService plantAreaService, Site site, PlantArea plantArea)
        {
            plantArea.SiteId = site.Id;
            plantAreaService.SavePlantArea(plantArea);
            return plantArea;
        }

        private static Equipment CreateEquipment(IEquipmentService equipmentService, PlantArea plantArea, Equipment equipment)
        {
            equipment.PlantAreaId = plantArea.Id;
            equipmentService.SaveEquipment(equipment);
            return equipment;
        }

        private static Instrument CreateInstrument(IInstrumentService instrumentService, PlantArea plantArea, Instrument instrument)
        {
            instrument.PlantAreaId = plantArea.Id;
            instrumentService.SaveInstrument(instrument);
            return instrument;
        }

        private static Schedule CreateSchedule(IScheduleService scheduleService, Equipment equipment, Schedule schedule)
        {
            schedule.EquipmentId = equipment.Id;
            scheduleService.SaveSchedule(schedule);
            return schedule;
        }

        private static CertificateHeader CreateCertificate(ICertificateService certificateService, Instrument instrument, CertificateHeader certificate)
        {
            certificate.InstrumentId = instrument.Id;
            certificateService.Save(certificate);
            return certificate;
        }
    }
}
