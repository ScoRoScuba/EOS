namespace EOS2.Web.Areas.Organizations.ViewModels.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Model;
    using EOS2.Web.ViewModels;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ChannelView", Justification = "The suggested change to ChannelviewModel... would obscure the real purpose of this class.")]
    public class ChannelViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int? SelectedChannelTypeId { get; set; }

        public IEnumerable<ReferenceDataType> ChannelTypes { get; set; }

        [Required]
        public int? SelectedEquipmentTypeId { get; set; }

        public IEnumerable<ReferenceDataType> Equipment { get; set; }

        [Required]
        public int? SelectedScheduleFrequencyId { get; set; }

        public IEnumerable<ReferenceDataType> ScheduleFrequency { get; set; }  
    }
}