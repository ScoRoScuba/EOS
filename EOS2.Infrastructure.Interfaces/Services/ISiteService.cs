namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface ISiteService
    {
        IEnumerable<Site> GetSitesFor(int customerId);

        ServiceResultDictionary Save(Site site);

        Site GetSite(int id);

        bool CustomerSiteExists(int customerId, string siteName);

        bool CustomerSiteExists(int customerId, string siteName, int siteIdToIgnore);
    }
}
