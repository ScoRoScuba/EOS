namespace EOS2.Infrastructure.Security
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    using EOS2.Common.Web;
    using EOS2.Identity.Model;
    using EOS2.Model.Enums;

    public class AuthorizationManager : ClaimsAuthorizationManager, IAuthorizationManager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Beta Authorization Check Code to be refactored")]
        public override bool CheckAccess(AuthorizationContext authorizationContext)
        {
            if (authorizationContext == null) throw new ArgumentNullException("authorizationContext");

            var resource = authorizationContext.Resource.First().Value;
            var action = authorizationContext.Action.First().Value;

            var route = RoutingFactory.Routes.GetRouteData(HttpContextFactory.Current);

            string controllersArea = route != null ? route.DataTokens["area"] as string : string.Empty;

            switch (controllersArea)
            {
                case "Organizations":
                    {
                        OrganizationType organizationTypeResource;
                        if (Enum.TryParse(resource, out organizationTypeResource))
                        {
                            switch (organizationTypeResource)
                            {
                                case OrganizationType.EOSOwner:
                                case OrganizationType.Customer:
                                    {
                                        return authorizationContext.Principal.HasClaim(
                                                x => x.Type == EOS2ClaimTypes.OrganizationType && (x.Value == OrganizationType.EOSOwner.ToString() || x.Value == OrganizationType.ServiceProvider.ToString() || x.Value == OrganizationType.Customer.ToString()));
                                    }

                                case OrganizationType.PortalAgent:
                                    {
                                        return authorizationContext.Principal.HasClaim(
                                                x => x.Type == EOS2ClaimTypes.OrganizationType && (x.Value == OrganizationType.EOSOwner.ToString()));                                        
                                    }

                                case OrganizationType.ServiceProvider:
                                    {
                                        return authorizationContext.Principal.HasClaim(
                                                x => x.Type == EOS2ClaimTypes.OrganizationType && (x.Value == OrganizationType.EOSOwner.ToString() || x.Value == OrganizationType.PortalAgent.ToString()));                                        
                                    }
                            }
                        }

                        return true;
                    }

                default:

                    if (resource == "Manage")
                    {
                        if (
                            authorizationContext.Principal.HasClaim(
                                x => x.Type == EOS2ClaimTypes.OrganizationType && x.Value == OrganizationType.ServiceProvider.ToString()))
                        {
                            return true;                            
                        }
                    }

                    if (resource == "Account" && action == "SignOut")
                    {
                        return true;
                    }

                    if (resource == "Security" && action == "AccessDenied")
                    {
                        return true;
                    }                    

                    if (resource == "Home" && action == "Index")
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }
    }
}
