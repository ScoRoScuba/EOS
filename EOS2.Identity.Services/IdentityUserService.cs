namespace EOS2.Identity.Services
{
    using EOS2.Identity.Model;

    using Microsoft.AspNet.Identity;

    public class IdentityUserService : UserManager<User, int>
    {
        public IdentityUserService(IUserStore<User, int> store)
            : base(store)
        {
        }
    }
}
