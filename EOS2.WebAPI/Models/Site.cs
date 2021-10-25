namespace EOS2.WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Site
    {
        /// <summary>
        /// The Unique Identifier of the Site 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Site
        /// </summary>
        [Required]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        public string Name { get; set; }

        /// <summary>
        /// The Address of the Site
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        /// <summary>
        /// The Postal Code of the Site
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }

        /// <summary>
        /// The Unique Identifier of the Customer of the Site
        /// </summary>
        [Required]
        public int CustomerId { get; set; }
    }
}