namespace EOS2.Web.Tests.TestStubs
{
    using System;
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class StubPlantAreaService : IPlantAreaService
    {
        private readonly bool plantAreaExistsReturnValue;

        public StubPlantAreaService(bool plantAreaExistsReturnValue)
        {
            this.plantAreaExistsReturnValue = plantAreaExistsReturnValue;
        }

        public PlantArea GetPlantArea(int plantAreaId)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary SavePlantArea(PlantArea plantArea)
        {
            throw new NotImplementedException();
        }

        public bool PlantAreaExists(string plantAreaName, int plantAreaId, int siteId)
        {
            return plantAreaExistsReturnValue;
        }

        public IEnumerable<PlantArea> GetPlantAreasForSite(int siteId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instrument> GetInstrumentsWithAvailableChannelsIn(int plantAreaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipment> GetEquipmentFor(int plantAreaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instrument> GetInstrumentsFor(int plantAreaId)
        {
            throw new NotImplementedException();
        }
    }
}
