namespace EOS2.Web.Areas.Organizations.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class FurnaceClassViewModel
    {
        [Required(ErrorMessage = "[[[Please select a Furnace Class]]]")]
        public int Id { get; set; }

        [Display(Name = "[[[Furnace Class]]]", Prompt = "[[[Furnace Class]]]")]
        [UIHint("I18nString")]
        public string Name { get; set; }
    }
}