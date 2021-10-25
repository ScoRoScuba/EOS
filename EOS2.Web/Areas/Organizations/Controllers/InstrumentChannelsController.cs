namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Common.Validation;
    using EOS2.Common.Web.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.InstrumentChannels;
    using EOS2.Web.Attributes.Filters;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class InstrumentChannelsController : BaseController
    {
        private readonly IChannelService channelService;
        private readonly IEditViewPartialModelBuilder<ChannelsViewModel> instrumentsChannelsViewModelBuilder;
        private readonly IDomainModelBuilder<Channel, ChannelViewModel> channelDomainModelBuilder;

        public InstrumentChannelsController(
            IChannelService channelService, 
            IEditViewPartialModelBuilder<ChannelsViewModel> instrumentsChannelsViewModelBuilder, 
            IDomainModelBuilder<Channel, ChannelViewModel> channelDomainModelBuilder)
        {
            if (channelService == null) throw new ArgumentNullException("channelService");
            if (instrumentsChannelsViewModelBuilder == null) throw new ArgumentNullException("instrumentsChannelsViewModelBuilder");
            if (channelDomainModelBuilder == null) throw new ArgumentNullException("channelDomainModelBuilder");

            this.channelService = channelService;
            this.instrumentsChannelsViewModelBuilder = instrumentsChannelsViewModelBuilder;
            this.channelDomainModelBuilder = channelDomainModelBuilder;
        }

        // GET: Organizations/Channel
        [TempDataRestoreModelStateFilter]
        public ActionResult View(int instrumentId)
        {
            var viewModel = instrumentsChannelsViewModelBuilder.Build(instrumentId);

            return View(viewModel);
        }

        public ActionResult CreateChannels(ChannelsViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.NumberOfChannels == 0) ModelState.AddModelError("NumberOfChannels", "Number of Channels can not be zero");                
            
            if (ModelState.IsValid)
            {
                if (viewModel.NumberOfChannels > 0)
                {
                    ServiceResultDictionary result = 
                        channelService.CreateDefaultSetOfChannelsForInstrument(
                                                            viewModel.NumberOfChannels, 
                                                            viewModel.InstrumentId, 
                                                            viewModel.SelectedChannelTypeId,
                                                            viewModel.SelectedEquipmentId,
                                                            viewModel.SelectedScheduleTypeId);

                    if (result.HasErrors)
                    {
                        ModelState.Merge(result);                        
                    }                    
                }
            }

            instrumentsChannelsViewModelBuilder.Rebuild(viewModel);

            return View("View", viewModel);
        }

        [HttpPost]
        [TempDataPersistModelStateFilter]
        public ActionResult SaveChannel(ChannelViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (ModelState.IsValid)
            {
                var channel = channelDomainModelBuilder.Build(viewModel);

                var result = channelService.SaveChannel(channel);

                if (!result.HasErrors)
                {
                    return RedirectToAction("View");                                         
                }

                ModelState.Merge(result);
            }

            ModelState.SetModelValue("ChannelId", new ValueProviderResult(viewModel.Id, string.Empty, CultureInfo.InvariantCulture));
            
            return this.RedirectToAction("View", viewModel.InstrumentId);
        }
    }
}