namespace EOS2.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Channel
    {
        /// <summary>
        /// The Unique Identifier of the Channel
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Channel
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Number of the Channel
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// The Type of the Channel
        /// </summary>
        [Required]
        public int ChannelTypeId { get; set; }

        /// <summary>
        /// The Unique Identifier of the Equipment attached to the Channel
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// The Unique Identifier of the Instrument of this Channel
        /// </summary>
        [Required]
        public int InstrumentId { get; set; }

        /// <summary>
        /// The Unique Identifier of the Schedule Frequency of this Channel
        /// </summary>
        public int ScheduleFrequencyId { get; set; }

        /// <summary>
        /// The Unique Identifier of the next Event the Channel is included in
        /// </summary>
        public int EventId { get; set; }
    }
}