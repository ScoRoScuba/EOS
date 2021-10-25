namespace EOS2.Identity.Model
{
    using System.Text;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string MiddleName { get; set; }

        public string FullName
        {
            get
            {
                var sb = new StringBuilder();
                
                sb.Append(Name);

                if (string.IsNullOrWhiteSpace(MiddleName))
                {
                    sb.Append(" ");
                }
                else
                {
                    sb.AppendFormat(" {0} ", MiddleName);                    
                }

                sb.Append(FamilyName);

                return sb.ToString();
            }
        }
    }
}
