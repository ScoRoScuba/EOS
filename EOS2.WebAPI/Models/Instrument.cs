namespace EOS2.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Instrument
    {
        /// <summary>
        /// Unique Identifier for Instrument
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name for Instrument
        /// </summary>
        [Required]
        [StringLength(120)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        /// Description for Instrument
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Notes for Instrument
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        /// <summary>
        /// Is SAT applicable to the Instrument 
        /// </summary>
        public bool IsSAT { get; set; }

        /// <summary>
        /// The number of Channels on the Instrument
        /// </summary>
        [Range(0, 100)]
        public int ChannelCount { get; set; }

        /// <summary>
        /// The Make or Manafacturer of the Instrument
        /// </summary>
        [StringLength(120)]
        [DataType(DataType.Text)]
        public string Make { get; set; }

        /// <summary>
        /// The model of Instrument
        /// </summary>
        [StringLength(120)]
        [DataType(DataType.Text)]
        public string Model { get; set; }

        /// <summary>
        /// Unique Serial Number for Instrument
        /// </summary>
        [StringLength(120)]
        [DataType(DataType.Text)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Is the Instrument regulated by AMS2750
        /// </summary>
        public bool IsAMS2750 { get; set; }

        /// <summary>
        /// Has the Instrument been removed
        /// </summary>
        public bool IsRemoved { get; set; }

        /// <summary>
        /// The Unique Identifier of the Instrument Type of the Instrument
        /// </summary>
        [Required]
        public int TypeId { get; set; }

        /// <summary>
        /// The Unique Identifier of the Frequency of the Instrument
        /// </summary>
        [Required]
        public int FrequencyId { get; set; }

        /// <summary>
        /// The Unique Identifier of the Plant Area of the Instrument
        /// </summary>
        [Required]
        public int PlantAreaId { get; set; }
    }
}