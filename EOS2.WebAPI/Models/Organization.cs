namespace EOS2.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Organization
    {
        /// <summary>
        /// The Unique Identifier of the Organization
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Organization
        /// </summary>
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        /// <summary>
        /// The Address of the Organization
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        /// <summary>
        /// The Postal Code of the Organization
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }
    }
}