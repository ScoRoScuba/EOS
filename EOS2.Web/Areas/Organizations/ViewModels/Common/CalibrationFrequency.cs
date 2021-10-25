namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;

    public class CalibrationFrequency
    {
        [Required(ErrorMessage = "[[[Please select a Calibration Frequency]]]")]
        public int Id { get; set; }

        [UIHint("I18nString")]
        public string Name { get; set; }

        public int DurationPosition { get; set; }
    }
}