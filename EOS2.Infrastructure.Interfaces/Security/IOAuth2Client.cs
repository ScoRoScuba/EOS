namespace EOS2.Infrastructure.Interfaces.Security 
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Name is as expected")]
    public interface IOAuth2Client
    {
        Uri GetAuthenticationUri(string state, string nonce, Dictionary<string, string> additionalValues);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "Passed as string")]
        Uri GetSignOutUri(string identityToken, string redirectUrl);

        Uri GetSignOutUri();

        Task<IAuthorizationToken> GetAuthorizationTokenAsync(string accessCode);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "ASYNC method returns a list of claims")]
        Task<IEnumerable<Claim>> GetUsersClaimsAsync(string accessToken);

        ClaimsPrincipal GetClaimsPrincipal(string accessToken);
    }
}
