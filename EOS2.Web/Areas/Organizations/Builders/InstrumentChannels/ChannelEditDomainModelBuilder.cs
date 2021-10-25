namespace EOS2.Web.Areas.Organizations.Builders.InstrumentChannels
{
    using System;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Builders;

    public class ChannelEditDomainModelBuilder : IDomainModelBuilder<Channel, ChannelViewModel>
    {
        private readonly IChannelService channelService;

        public ChannelEditDomainModelBuilder(IChannelService channelService)
        {
            if (channelService == null) throw new ArgumentNullException("channelService"); 

            this.channelService = channelService;
        }

        public Channel Build(ChannelViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            var channel = new Channel();

            if (viewModel.Id > 0)
            {
                channel = channelService.GetChannelById(viewModel.Id);
            }

            channel.Name = channel.Name != viewModel.Name ? viewModel.Name : channel.Name;
            channel.Number = channel.Name != viewModel.Number ? viewModel.Number : channel.Number;

            channel.TypeId = channel.TypeId != viewModel.SelectedChannelTypeId ? (viewModel.SelectedChannelTypeId == 0 ? (int?)null : viewModel.SelectedChannelTypeId) : channel.TypeId;
            channel.ConnectedToEquipmentId = channel.ConnectedToEquipmentId != viewModel.SelectedEquipmentTypeId ? (viewModel.SelectedEquipmentTypeId == 0 ? (int?)null : viewModel.SelectedEquipmentTypeId) : channel.ConnectedToEquipmentId;
            channel.ScheduleFrequencyId = channel.ScheduleFrequencyId != viewModel.SelectedScheduleFrequencyId ? (viewModel.SelectedScheduleFrequencyId == 0 ? (int?)null : viewModel.SelectedScheduleFrequencyId) : channel.ScheduleFrequencyId;
    
            return channel;
        }
    }
}