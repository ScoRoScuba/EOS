namespace EOS2.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EOS2.Identity.Model;

    public interface IAuthorizationSecurityService
    {
        Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(
            string userName,
            string authenticationMethod,
            IList<Claim> claims);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Design Choice")]
        Task<List<Claim>> BuildClaimsAsync(User user);

        Task<ClaimsPrincipal> CreateUsersSecurityClaimsPrincipalAsync(User user);
    }
}
