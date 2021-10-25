namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;

    public class Instrument
    {
        public int Id { get; set; }

        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Instrument]]]")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between InstrumentType & object.GetType()")]
        [Display(Name = "[[[Type]]]", Prompt = "[[[Type of Instrument]]]")]
        public InstrumentType Type { get; set; }

        [Display(Name = "[[[Model]]]")]
        public string Model { get; set; }

        [Display(Name = "[[[Serial Number]]]")]
        public string SerialNumber { get; set; }
    }
}