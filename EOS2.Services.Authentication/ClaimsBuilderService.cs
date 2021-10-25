namespace EOS2.Services.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;

    using EOS2.Identity.Model;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model;

    using Microsoft.AspNet.Identity;

    public class ClaimsBuilderService : IClaimsBuilderService
    {
        private IOrganizationsService organizationsService;

        private IUserAppSession userApplicationSession;

        private ILoggerService loggerService;

        private IdentityUserService identityUserService;

        public ClaimsBuilderService(
            IOrganizationsService organizationsService,
            IUserAppSession userApplicationSession,
            IdentityUserService identityUserService,
            ILoggerService loggerService)
        {
            if (organizationsService == null) throw new ArgumentNullException("organizationsService");
            if (userApplicationSession == null) throw new ArgumentNullException("userApplicationSession");
            if (loggerService == null) throw new ArgumentNullException("loggerService");
            if (identityUserService == null) throw new ArgumentNullException("identityUserService");

            this.organizationsService = organizationsService;
            this.userApplicationSession = userApplicationSession;
            this.loggerService = loggerService;
            this.identityUserService = identityUserService;
        }

        public IEnumerable<Claim> GetUsersApplicationClaims(int userId)
        {
            loggerService.Log("GetApplicationClaims - Starting");

            var claims = new List<Claim>();

            var usersOrganizationRoles = organizationsService.GetUsersOrganizationalRoles(userId);
            
            //// TODO special case testing for the test data stuff below.  WE NEED TO REMOVE THIS CONTAINING (IF)                    
            var organizationRoles = usersOrganizationRoles as IList<OrganizationRole> ?? usersOrganizationRoles.ToList();
            if (organizationRoles.Any())
            {
                userApplicationSession.CurrentOrganizationType = organizationRoles.First()
                    .OrganizationType;
                userApplicationSession.CurrentOrganization = organizationRoles.First()
                    .Organization;

                claims = organizationRoles.Select(
                                usersOrganizationRole => new Claim(
                                                                EOS2ClaimTypes.OrganizationType,
                                                                usersOrganizationRole.OrganizationType.ToString())).ToList();
            }

            loggerService.Log("GetApplicationClaims - Finsihed");

            return claims;
        }

        public IEnumerable<Claim> GetUsersClaims(int userId)
        {
            var claims = new List<Claim>();

            var user = identityUserService.FindById(userId);

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

            if (claims.Any(p => p.Type != ClaimTypes.Actor))
            {
                claims.Add(new Claim(ClaimTypes.Actor, "EOS2User"));
            }

            return claims;
        }
    }
}
