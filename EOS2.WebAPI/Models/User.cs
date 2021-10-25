namespace EOS2.WebAPI.Models
{
    public class User
    {
        /// <summary>
        /// The Unique Identifier of the User
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The User Name of the User
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The Given Name of the User
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Family Name of the User
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// The Middle Name of the User
        /// </summary>
        public string MiddleName { get; set; }
    }
}