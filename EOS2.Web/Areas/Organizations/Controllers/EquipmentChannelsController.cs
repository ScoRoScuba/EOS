namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Web.Mvc;

    using EOS2.Common.Web.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.Builders.EquipmentChannels;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.EquipmentChannels;
    using EOS2.Web.Attributes.Filters;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class EquipmentChannelsController : BaseController
    {
        private readonly IEquipmentService equipmentService;

        private readonly IInstrumentService instrumentService;
        private readonly IViewModelWithQueryBuilder<ChannelsAllocatedToEquipmentCriteria, EquipmentChannelsViewModel> channelsAllocatedToEquipmentViewModelBuilder;
        private readonly IViewModelWithQueryBuilder<UnallocatedChannelsForInstrumentCriteria, EquipmentChannelsViewModel> unattachedChannelsViewModelBuilder;

        public EquipmentChannelsController(
            IEquipmentService equipmentService,
            IInstrumentService instrumentService,
            IViewModelWithQueryBuilder<ChannelsAllocatedToEquipmentCriteria, EquipmentChannelsViewModel> channelsAllocatedToEquipmentViewModelBuilder,
            IViewModelWithQueryBuilder<UnallocatedChannelsForInstrumentCriteria, EquipmentChannelsViewModel> unattachedChannelsViewModelBuilder)
        {
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");
            if (equipmentService == null) throw new ArgumentNullException("equipmentService");
            if (unattachedChannelsViewModelBuilder == null) throw new ArgumentNullException("unattachedChannelsViewModelBuilder");
            if (channelsAllocatedToEquipmentViewModelBuilder == null) throw new ArgumentNullException("channelsAllocatedToEquipmentViewModelBuilder");

            this.instrumentService = instrumentService;
            this.equipmentService = equipmentService;
            this.channelsAllocatedToEquipmentViewModelBuilder = channelsAllocatedToEquipmentViewModelBuilder;
            this.unattachedChannelsViewModelBuilder = unattachedChannelsViewModelBuilder;
        }

        [TempDataRestoreModelStateFilter]
        public ActionResult View(int instrumentId, int equipmentId)
        {
            var equipment = equipmentService.GetEquipment(equipmentId);

            var instrument = instrumentService.GetInstrument(instrumentId);

            var viewModel = channelsAllocatedToEquipmentViewModelBuilder.Build(new ChannelsAllocatedToEquipmentCriteria(equipment, instrument));

            return this.View("view", viewModel);
        }

        [HttpPost]
        [TempDataPersistModelStateFilter]
        public ActionResult SaveChannel(ChannelViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.SelectedEquipmentTypeId.HasValue && viewModel.SelectedEquipmentTypeId == 0)
            {
                equipmentService.DeallocateChannel(viewModel.Id);                
            } 

            return RedirectToAction("View");                                         
        }

        [TempDataRestoreModelStateFilter]
        public ActionResult AvailableChannels(int instrumentId, int equipmentId)
        {
            var viewModel = unattachedChannelsViewModelBuilder.Build(new UnallocatedChannelsForInstrumentCriteria(instrumentId, equipmentId));

            return this.View(viewModel);
        }

        [HttpPost]
        [TempDataPersistModelStateFilter]
        public ActionResult AllocateChannel(ChannelViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.SelectedEquipmentTypeId.HasValue && viewModel.SelectedEquipmentTypeId != 0)
            {
                equipmentService.AllocateToChannel(viewModel.SelectedEquipmentTypeId.Value, viewModel.Id);                
            } 

            return RedirectToAction("AvailableChannels");            
        }
    }
}