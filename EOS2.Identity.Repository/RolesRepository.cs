namespace EOS2.Identity.Repository
{
    using EOS2.Identity.Model;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityRolesRepository : RoleStore<Role, int, UserRole>
    {
        public IdentityRolesRepository(EOSIdentityDbContext context)
            : base(context)
        {
        }
    }
}
