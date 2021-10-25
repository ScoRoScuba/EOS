
namespace EOS2.Infrastructure.Security
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Services;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using EOS2.Identity.Model;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces;

    using Thinktecture.IdentityModel;

    public class AuthorizationSecurityService : IAuthorizationSecurityService
    {
        private readonly IdentityRoleService identityRoleService;

        public AuthorizationSecurityService( IdentityRoleService identityRoleService)
        {
            this.identityRoleService = identityRoleService;
        }

        public async Task<IList<Claim>> BuildClaimsAsync(User user)
        {
            var claims = new List<Claim>();

            foreach (var userRole in user.Roles)
            {
                var role = await identityRoleService.FindByIdAsync(userRole.RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims;
        }

        public async Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(string userName, string authenticationMethod, IList<Claim> claims)
        {
            if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName");
            if(string.IsNullOrWhiteSpace(authenticationMethod)) throw new ArgumentNullException("authenticationMethod");

            if (claims == null)
            {
                claims = new List<Claim>();
            }

            if (claims.All(p => p.Type != ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
            }

            if (!claims.All( p => p.Type == ClaimTypes.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
            }

            if (!claims.All(p => p.Type == ClaimTypes.AuthenticationMethod))
            {
                claims.Add(new Claim(ClaimTypes.AuthenticationMethod, authenticationMethod));
            }

            claims.Add(AuthenticationInstantClaim.Now);

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "EOS.IdentityProvider"));

            return FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate(string.Empty, principal);

        }
    }
}
