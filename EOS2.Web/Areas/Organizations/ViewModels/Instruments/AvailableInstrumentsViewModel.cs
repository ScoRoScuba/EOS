namespace EOS2.Web.Areas.Organizations.ViewModels.Instruments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using EOS2.Web.ViewModels;

    public class AvailableInstrumentsViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "[[[Please select an Instrument]]]")]
        public int SelectedInstrumentId { get; set; }

        public SelectList AvailableInstruments { get; set; }
    }
}