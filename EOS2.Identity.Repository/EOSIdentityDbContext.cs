namespace EOS2.Identity.Repository
{
    using EOS2.Identity.Model;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class EOSIdentityDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public EOSIdentityDbContext()
        {
        }

        public EOSIdentityDbContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
        }
    }
}
