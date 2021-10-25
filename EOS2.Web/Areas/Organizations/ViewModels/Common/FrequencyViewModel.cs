namespace EOS2.Web.Areas.Organizations.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class FrequencyViewModel
    {
        [Required(ErrorMessage = "[[[Please select a Frequency]]]")]
        public int Id { get; set; }

        [UIHint("I18nString")]
        public string Name { get; set; }

        public int DurationPosition { get; set; }
    }
}