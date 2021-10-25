namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Model;
    using EOS2.Web.ViewModels;

    public class ChannelViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "[[[Name is required]]]")]
        public string Name { get; set; }

        [Required(ErrorMessage = "[[[Number is required]]]")]
        public string Number { get; set; }

        [Required(ErrorMessage = "[[[Channel type is required]]]")]
        public int SelectedChannelTypeId { get; set; }

        public IEnumerable<ReferenceDataType> ChannelTypes { get; set; }

        [Required(ErrorMessage = "[[[Equipment type is required]]]")]
        public int SelectedEquipmentTypeId { get; set; }

        public IEnumerable<ReferenceDataType> Equipment { get; set; }

        [Required(ErrorMessage = "[[[Schedule is required]]]")]
        public int SelectedScheduleFrequencyId { get; set; }

        public IEnumerable<ReferenceDataType> ScheduleFrequency { get; set; }  
    }
}