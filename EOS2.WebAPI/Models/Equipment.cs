namespace EOS2.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Equipment
    {
        /// <summary>
        /// The Unique Identifier of the piece of Equipment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the piece of Equipment
        /// </summary>
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        /// <summary>
        /// The Description of the piece of Equipment
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Make of the piece of Equipment
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// The Model of the piece of Equipment
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The Serial Number of the piece of Equipment
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// The Unique Identifier of the Equipment Type of the piece of Equipment
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// The Notes of the piece of Equipment
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The Unique Identifier of the Plant Area of the piece of Equipment
        /// </summary>
        public int PlantAreaId { get; set; }
    }
}