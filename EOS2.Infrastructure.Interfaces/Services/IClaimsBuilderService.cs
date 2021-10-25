namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IClaimsBuilderService
    {
        IEnumerable<Claim> GetUsersApplicationClaims(int userId);

        IEnumerable<Claim> GetUsersClaims(int userId);
    }
}
