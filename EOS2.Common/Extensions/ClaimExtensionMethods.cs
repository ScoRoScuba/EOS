namespace EOS2.Common.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using System.Security.Claims;

    using EOS2.Identity.Model;
    using EOS2.Model.Enums;

    public static class ClaimExtensionMethods
    {
        public static bool HasClaim(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.Any(c => c.Type == claimType);
        }

        public static Claim GetClaim(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.SingleOrDefault(c => c.Type == claimType);
        }

        public static bool HasClaim(this IEnumerable<Claim> claims, string claimType, string claim)
        {
            return claims.Any(c => c.Type == claimType && c.Value == claim);
        }

        public static bool HasOrganizationClaimOf(this IEnumerable<Claim> claims, OrganizationType organizationType)
        {
            return claims.Any(c => c.Type == EOS2ClaimTypes.OrganizationType && c.Value == organizationType.ToString());
        }

        public static int HasOrganizationClaimCountOf(this IEnumerable<Claim> claims)
        {
            return claims.Count(c => c.Type == EOS2ClaimTypes.OrganizationType);
        }
    }
}
