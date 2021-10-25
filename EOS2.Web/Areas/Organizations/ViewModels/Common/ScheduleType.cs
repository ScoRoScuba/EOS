namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;

    public class ScheduleType
    {
        public int Id { get; set; }

        [UIHint("I18nString")]
        public string Name { get; set; }
    }
}