namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.Collections.Generic;

    public class InstrumentsForPlantAreaViewModel
    {
        public int PlantAreaId { get; set; }

        public IEnumerable<InstrumentViewModel> Instruments { get; set; }
    }
}