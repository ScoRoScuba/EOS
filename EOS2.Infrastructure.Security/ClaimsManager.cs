namespace EOS2.Infrastructure.Security
{
    using System.Security.Claims;

    using EOS2.Common.Exceptions;
    using EOS2.Common.Extensions;
    using EOS2.Infrastructure.DependencyInjection;
    using EOS2.Infrastructure.Interfaces.Services;
    using Microsoft.Practices.Unity;

    public class ClaimsManager : ClaimsAuthenticationManager
    {
        // we use this one to create  EOS specific claims

        private IClaimsBuilderService claimsBuilderService;

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            IntialiseAuthenticationManager();

            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated == true)
            {
                if (incomingPrincipal.Claims.HasClaim("sub"))
                {
                    var claimUserId = incomingPrincipal.Claims.GetClaim("sub");
                    int userId = int.Parse(claimUserId.Value);
                    
                    var claims = claimsBuilderService.GetUsersApplicationClaims(userId);

                    ((ClaimsIdentity)incomingPrincipal.Identity).AddClaims(claims);
                }
            }

            return incomingPrincipal;
        }

        private void IntialiseAuthenticationManager()
        {
            // we do these here because doing them in the constructor gets us an invalid object because this dll is loaded before the web site
            // and before the IIS Session exists.
            claimsBuilderService = UnityConfig.ConfiguredContainer.Resolve<IClaimsBuilderService>();
            if (claimsBuilderService == null) throw new DependencyResolutionException(typeof(IClaimsBuilderService), "claimsBuilderService");            
        }
    }
}
