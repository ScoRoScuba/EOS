namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class PlantAreaService : IPlantAreaService
    {
        private readonly IRepository<PlantArea> plantAreaRepository;

        public PlantAreaService(IRepository<PlantArea> plantAreaRepository)
        {
            if (plantAreaRepository == null) throw new ArgumentNullException("plantAreaRepository");

            this.plantAreaRepository = plantAreaRepository;
        }

        public ServiceResultDictionary SavePlantArea(PlantArea plantArea)
        {
            if (plantArea == null) throw new ArgumentNullException("plantArea");

            var serviceResult = new ServiceResultDictionary();

            if (plantArea.Id > 0)
            {
                this.plantAreaRepository.Update(plantArea);
            }
            else
            {
                this.plantAreaRepository.Add(plantArea);
            }

            return serviceResult;
        }

        public PlantArea GetPlantArea(int plantAreaId)
        {
            var plantArea = this.plantAreaRepository.Find(pa => pa.Id == plantAreaId);

            return plantArea;
        }

        public IEnumerable<PlantArea> GetPlantAreasForSite(int siteId)
        {
            return this.plantAreaRepository.FindAll(pa => pa.SiteId == siteId);           
        }

        public bool PlantAreaExists(string plantAreaName, int plantAreaId, int siteId)
        {
            return this.plantAreaRepository.Find(pa => pa.SiteId == siteId && pa.Name.ToLower().Trim() == plantAreaName.ToLower().Trim() && pa.Id != plantAreaId) != null;
        }

        public IEnumerable<Equipment> GetEquipmentFor(int plantAreaId)
        {
            var plantArea = this.plantAreaRepository.Find(p => p.Id == plantAreaId);
            
            return plantArea != null ? plantArea.Equipments.ToList() : new List<Equipment>();
        }

        public IEnumerable<Instrument> GetInstrumentsWithAvailableChannelsIn(int plantAreaId)
        {
            var instruments = plantAreaRepository.Find(p => p.Id == plantAreaId)
                .Instruments.Where(i => i.Channels.Any(c => !c.ConnectedToEquipmentId.HasValue));

            return instruments;
        }

        public IEnumerable<Instrument> GetInstrumentsFor(int plantAreaId)
        {
            var plantArea = this.plantAreaRepository.Find(i => i.Id == plantAreaId);
            return plantArea != null ? plantArea.Instruments.ToList() : new List<Instrument>();
        }
    }
}
