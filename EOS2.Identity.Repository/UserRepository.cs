namespace EOS2.Identity.Repository
{
    using EOS2.Identity.Model;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserRepository : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserRepository(EOSIdentityDbContext context)
            : base(context)
        {
        }
    }
}
