﻿namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.Collections.Generic;

    public class EquipmentForPlantAreaViewModel
    {
        public int PlantAreaId { get; set; }

        public IEnumerable<EquipmentViewModel> Equipment { get; set; }
    }
}