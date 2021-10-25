namespace EOS2.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PlantArea
    {
        /// <summary>
        /// The Unique Identifier of the Plant Area
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Plant Area
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Unique Identifier of the Site for the Plant Area
        /// </summary>
        [Required]
        public int SiteId { get; set; }
    }
}