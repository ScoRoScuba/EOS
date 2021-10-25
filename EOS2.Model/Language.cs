namespace EOS2.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class Language 
    {
        public string Key { get; set; }

        public string NativeNameTitleCase { get; set; }
    }
}
