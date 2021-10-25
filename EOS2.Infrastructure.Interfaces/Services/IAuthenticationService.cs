namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EOS2.Common.Validation;
    using EOS2.Identity.Model;

    public interface IAuthenticationService
    {
        Task<User> ValidateUserAsync(string userName, string password);

        Task<AuthenticationStatus> SignInAsync(string userName, string password, bool rememberMe);

        void SignOut();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Design Choice")]
        Task<List<Claim>> BuildClaimsAsync(User user);

        ClaimsPrincipal CreateClaimsPrincipal(
            User user,
            string authenticationMethod,
            IList<Claim> claims);

        Task<ClaimsPrincipal> CreateUsersSecurityClaimsPrincipalAsync(User user);

        Task<AuthenticationStatus> GetUserAuthenticationStatusAsync(string userName);

        ServiceResultDictionary CreateUser(User user);

        ServiceResultDictionary UpdateUser(User user);

        ServiceResultDictionary SetPassword(User user, string password);

        ServiceResultDictionary SetPassword(int userId, string password);
    }
}
