namespace EOS2.WebAPI.Models
{
    public class Engineer 
    {
        // TODO: Might have this inheriting from User and just adding in PIN info, not sure yet what eCat needs

        /// <summary>
        /// The Unique Identifier of the eCAT Engineer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The User Name of the eCAT Engineer
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The Hashed PIN of the eCAT Engineer
        /// </summary>
        public string PIN { get; set; }

        /// <summary>
        /// The Unique Identifier of the Service Provider of the Engineer
        /// </summary>
        public int ServiceProviderId { get; set; }
    }
}