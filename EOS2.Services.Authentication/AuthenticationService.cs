namespace EOS2.Services.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IdentityModel.Services;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    using EOS2.Common.Validation;
    using EOS2.Identity.Model;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;

    using Microsoft.AspNet.Identity;

    using Thinktecture.IdentityModel;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IdentityUserService identityUserService;

        private readonly IdentityRoleService identityRoleService;

        private readonly IUserAppSession userApplicationSession;

        public AuthenticationService(IUserAppSession userApplicationSession, IdentityUserService identityUserService, IdentityRoleService identityRoleService)
        {
            if (identityRoleService == null) throw new ArgumentNullException("identityRoleService");
            if (identityUserService == null) throw new ArgumentNullException("identityUserService");
            if (userApplicationSession == null) throw new ArgumentNullException("userApplicationSession");

            this.identityUserService = identityUserService;
            this.identityRoleService = identityRoleService;
            this.userApplicationSession = userApplicationSession;
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            return await identityUserService.FindAsync(userName, password);
        }

        public async Task<AuthenticationStatus> SignInAsync(string userName, string password, bool rememberMe)
        {
            var user = await this.ValidateUserAsync(userName, password);
            if (user != null)
            {
                var claimsPrincipal = await CreateUsersSecurityClaimsPrincipalAsync(user);

                CreateAuthenticatedSesssionToken(claimsPrincipal, rememberMe);

                userApplicationSession.CurrentUser = user;

                return AuthenticationStatus.Success;
            }
            else
            {
                return await GetUserAuthenticationStatusAsync(userName);
            }
        }

        public void SignOut()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            FederatedAuthentication.SessionAuthenticationModule.DeleteSessionTokenCookie();

            ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.ToList()
                .ForEach((claim) => ((ClaimsIdentity)HttpContext.Current.User.Identity).RemoveClaim(claim));
        }

        public async Task<AuthenticationStatus> GetUserAuthenticationStatusAsync(string userName)
        {
            var checkUser = await identityUserService.FindByNameAsync(userName);

            if (checkUser == null)
            {
                return AuthenticationStatus.Unknown;
            }

            if (checkUser.LockoutEnabled)
            {
                return AuthenticationStatus.Locked;
            }

            if (!checkUser.EmailConfirmed)
            {
                return AuthenticationStatus.Locked;
            }

            return AuthenticationStatus.Failed;            
        }

        public async Task<List<Claim>> BuildClaimsAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            var claims = new List<Claim>();

            foreach (var userRole in user.Roles)
            {
                var role = await identityRoleService.FindByIdAsync(userRole.RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims;
        }

        public ClaimsPrincipal CreateClaimsPrincipal(User user, string authenticationMethod, IList<Claim> claims)
        {
            if (user == null) throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(authenticationMethod))
            {
                throw new ArgumentNullException("authenticationMethod");
            }

            if (claims == null)
            {
                claims = new List<Claim>();
            }

            if (claims.All(p => p.Type != EOS2ClaimTypes.UserIdentifier))
            {
                claims.Add(new Claim(EOS2ClaimTypes.UserIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)));
            }

            if (claims.All(p => p.Type != ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            }

            if (claims.Any(p => p.Type != ClaimTypes.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            }

            if (claims.Any(p => p.Type != ClaimTypes.AuthenticationMethod))
            {
                claims.Add(new Claim(ClaimTypes.AuthenticationMethod, authenticationMethod));
            }

            if (claims.Any(p => p.Type != ClaimTypes.Actor))
            {
                claims.Add(new Claim(ClaimTypes.Actor, "EOS2User"));
            }

            claims.Add(AuthenticationInstantClaim.Now);

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "EOS.IdentityProvider"));

            return FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate(string.Empty, principal);
        }

        public async Task<ClaimsPrincipal> CreateUsersSecurityClaimsPrincipalAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            var claims = await BuildClaimsAsync(user);

            var claimsPrincipal = CreateClaimsPrincipal(user, AuthenticationMethods.Password, claims);

            return claimsPrincipal;
        }

        public ServiceResultDictionary CreateUser(User user)
        {
            var serviceResult = new ServiceResultDictionary();

            var identityResult = identityUserService.Create(user);

            if (identityResult.Errors.Any())
            {
                var state = new ServiceState();
                foreach (var error in identityResult.Errors)
                {
                    state.Errors.Add(error);                    
                }

                serviceResult.Add("UserName", state);
            }                       

            return serviceResult;
        }

        public ServiceResultDictionary SetPassword(User user, string password)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password");

            return SetPassword(user.Id, password);
        }

        public ServiceResultDictionary SetPassword(int userId, string password)
        {
            var serviceResult = new ServiceResultDictionary();

            var identityResult = identityUserService.AddPassword(userId, password);

            if (identityResult.Errors.Any())
            {
                var state = new ServiceState();
                foreach (var error in identityResult.Errors)
                {
                    state.Errors.Add(error);                    
                }

                serviceResult.Add("Auth", state);
            }                       

            return serviceResult;
        }

        public ServiceResultDictionary UpdateUser(User user)
        {
            var serviceResult = new ServiceResultDictionary();

            var identityResult = identityUserService.Update(user);

            if (identityResult.Errors.Any())
            {
                var state = new ServiceState();
                foreach (var error in identityResult.Errors)
                {
                    state.Errors.Add(error);                    
                }

                serviceResult.Add("Auth", state);
            }                       

            return serviceResult;
        }

        private static void CreateAuthenticatedSesssionToken(ClaimsPrincipal claimsPrincipal, bool rememberMe)
        {
            if (claimsPrincipal == null) throw new ArgumentNullException("claimsPrincipal");

            var sessionToken = new SessionSecurityToken(claimsPrincipal)
            {
                IsPersistent = rememberMe
            };

            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionToken);
        }
    }
}
