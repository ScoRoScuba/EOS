namespace EOS2.Identity.Services
{
    using EOS2.Identity.Model;

    using Microsoft.AspNet.Identity;

    public class IdentityRoleService : RoleManager<Role, int>
    {
        public IdentityRoleService(IRoleStore<Role, int> store)
            : base(store)
        {
        }
    }
}
