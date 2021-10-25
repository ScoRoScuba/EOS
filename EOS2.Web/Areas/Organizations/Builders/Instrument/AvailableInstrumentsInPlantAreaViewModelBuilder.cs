namespace EOS2.Web.Areas.Organizations.Builders.Instrument
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Builders;

    public class AvailableInstrumentsInPlantAreaViewModelBuilder : IEditViewModelBuilder<AvailableInstrumentsViewModel>
    {
        private readonly IPlantAreaService plantAreaService;

        public AvailableInstrumentsInPlantAreaViewModelBuilder(IPlantAreaService plantAreaService)
        {
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");
            
            this.plantAreaService = plantAreaService;
        }

        public AvailableInstrumentsViewModel Build(int? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            return new AvailableInstrumentsViewModel()
                                {
                                    AvailableInstruments = GetAvailableInstruments(id.Value)
                                };
        }

        private SelectList GetAvailableInstruments(int plantAreaId)
        {
            var availableInstruments = plantAreaService.GetInstrumentsWithAvailableChannelsIn(plantAreaId).ToList();

            if (availableInstruments.Any())
            {
                var selectListItems = 
                        availableInstruments
                        .Select(i => new SelectListItem { Value = i.Id.ToString(CultureInfo.InvariantCulture), Text = i.Name })
                        .ToList();

                selectListItems.Insert(
                    0,
                    new SelectListItem { Selected = true, Value = string.Empty, Text = "[[[Select Instrument]]]" });

                return new SelectList(selectListItems);
            }

            return new SelectList(new List<SelectListItem> { new SelectListItem { Selected = true, Value = string.Empty, Text = "[[[Select Instrument]]]" } });                
        }
    }
}