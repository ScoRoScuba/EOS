namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Model;

    public class InstrumentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Instrument]]]")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between InstrumentType & object.GetType()")]
        [Display(Name = "[[[Type]]]", Prompt = "[[[Type of Instrument]]]")]
        public InstrumentTypeViewModel Type { get; set; }

        [Display(Name = "[[[Model]]]")]
        public string Model { get; set; }

        [Display(Name = "[[[Serial Number]]]")]
        public string SerialNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Channels set by AutoMapper")]
        public virtual ICollection<Channel> Channels { get; set; }
    }
}