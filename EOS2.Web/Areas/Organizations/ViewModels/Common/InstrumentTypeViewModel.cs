namespace EOS2.Web.Areas.Organizations.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class InstrumentTypeViewModel
    {
        [Required(ErrorMessage = "[[[Please select a Type]]]")]
        public int Id { get; set; }

        [UIHint("I18nString")]
        public string Name { get; set; }
    }
}