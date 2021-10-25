namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class SiteService : ISiteService
    {
        private readonly IRepository<Site> repository;

        public SiteService(IRepository<Site> repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");

            this.repository = repository;
        }

        public IEnumerable<Site> GetSitesFor(int customerId)
        {
            return repository.FindAll(s => s.OrganizationId == customerId);
        }

        public ServiceResultDictionary Save(Site site)
        {
            if (site == null) throw new ArgumentNullException("site");

            var serviceResult = new ServiceResultDictionary();

            if (site.Id > 0)
            {
                repository.Update(site);    
            }
            else
            {
                repository.Add(site);    
            }

            return serviceResult;
        }

        public Site GetSite(int id)
        {
            return repository.Find(s => s.Id == id);
        }

        public bool CustomerSiteExists(int customerId, string siteName)
        {
            return this.repository.Find(s => s.OrganizationId == customerId && s.Name.ToLower().Trim() == siteName.ToLower().Trim()) != null;
        }

        public bool CustomerSiteExists(int customerId, string siteName, int siteIdToIgnore)
        {
            return this.repository.Find(s => s.OrganizationId == customerId && 
                                             s.Name.ToLower().Trim() == siteName.ToLower().Trim()
                                             && s.Id != siteIdToIgnore) != null;
        }
    }
}
