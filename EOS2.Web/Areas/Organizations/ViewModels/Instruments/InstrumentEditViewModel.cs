namespace EOS2.Web.Areas.Organizations.ViewModels.Instruments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;   

    [UniqueInstrument]
    public class InstrumentEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string StoredName { get; set; }

        [Required(ErrorMessage = "[[[Please enter the name for your instrument]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Instrument]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Description]]]", Prompt = "[[[Description of Instrument]]]")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "[[[Notes]]]", Prompt = "[[[Notes for Instrument]]]")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between InstrumentType & object.GetType()")]
        [Display(Name = "[[[Instrument Type]]]", Prompt = "[[[Type of Instrument]]]")]
        public InstrumentTypeViewModel Type { get; set; }

        [Display(Name = "[[[Calibration Frequency]]]", Prompt = "[[[Frequency of Calibration of Instrument]]]")]
        public CalibrationFrequencyViewModel CalibrationFrequency { get; set; }

        [Display(Name = "[[[Subject to SAT]]]", Prompt = "[[[Is Instrument subject to SAT]]]")]
        public bool IsSAT { get; set; }

        [Display(Name = "[[[Number of Channels]]]", Prompt = "[[[Number of channels on Instrument]]]")]
        [Range(0, 100, ErrorMessage = "[[[Number of Channels must be between 0 and 100]]]")]
        public int ChannelCount { get; set; }

        [Display(Name = "[[[Make]]]", Prompt = "[[[Make of Instrument]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Make { get; set; }

        [Display(Name = "[[[Model]]]", Prompt = "[[[Model of Instrument]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Model { get; set; }

        [Display(Name = "[[[Serial Number]]]", Prompt = "[[[Serial Number of Instrument]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string SerialNumber { get; set; }

        [Display(Name = "[[[AMS2750 Instrument]]]", Prompt = "[[[Is an AMS2750 Instrument]]]")]
        public bool IsAMS2750 { get; set; }

        [Display(Name = "[[[Instrument Removed]]]", Prompt = "[[[Has the Instrument been removed]]]")]
        public bool IsRemoved { get; set; }

        // Reference Data
        public IEnumerable<InstrumentTypeViewModel> InstrumentTypes { get; set; }

        public IEnumerable<CalibrationFrequencyViewModel> CalibrationFrequencies { get; set; }

        public IEnumerable<CertificateViewModel> Certificates { get; set; }
    }
}