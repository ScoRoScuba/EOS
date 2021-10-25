namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;

    public class PlantArea
    {
        public int Id { get; set; }
        
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Plant Area]]]")]
        public string Name { get; set; }
    }
}