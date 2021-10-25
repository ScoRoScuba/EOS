namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface IPlantAreaService
    {
        ServiceResultDictionary SavePlantArea(PlantArea plantArea);

        PlantArea GetPlantArea(int plantAreaId);

        bool PlantAreaExists(string plantAreaName, int plantAreaId, int siteId);

        IEnumerable<PlantArea> GetPlantAreasForSite(int siteId);

        IEnumerable<Instrument> GetInstrumentsFor(int plantAreaId);

        IEnumerable<Equipment> GetEquipmentFor(int plantAreaId);

        IEnumerable<Instrument> GetInstrumentsWithAvailableChannelsIn(int plantAreaId);
    }
}
