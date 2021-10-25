namespace EOS2.Web.Areas.Organizations.ViewModels.InstrumentChannels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.ViewModels;

    public class ChannelsViewModel : BaseViewModel
    {
        public InstrumentEditViewModel Instrument { get; set; }

        public int NumberOfChannels { get; set; }

        [Required(ErrorMessage = "[[[Channel type is Required]]]")]
        public int SelectedChannelTypeId { get; set; }

        [Display(Name = "[[[Channel Type]]]", Prompt = "[[[Type of Channel]]]")]
        public IEnumerable<ReferenceDataType> ChannelTypes { get; set; }

        [Required(ErrorMessage = "[[[Equipment is Required]]]")]
        public int SelectedEquipmentId { get; set; }

        [Display(Name = "[[[Attached to equipment]]]", Prompt = "[[[Equipment the channel is connected]]]")]
        public IEnumerable<ReferenceDataType> Equipment { get; set; }

        [Required(ErrorMessage = "[[[Schedule type must be set]]]")]
        public int SelectedScheduleTypeId { get; set; }

        [Display(Name = "[[[Calibration Schedule]]]", Prompt = "[[[Calibration Schedule]]]")]
        public IEnumerable<ReferenceDataType> ScheduleType { get; set; }  

        public IEnumerable<ChannelViewModel> Channels { get; set; }
    }
}