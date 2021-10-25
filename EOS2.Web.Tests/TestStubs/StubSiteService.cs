namespace EOS2.Web.Tests.TestStubs
{
    using System;
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;

    public class StubSiteService : ISiteService
    {
        private readonly bool customerSiteExistsReturnValue;

        public StubSiteService(bool customerSiteExistsReturnValue)
        {
            this.customerSiteExistsReturnValue = customerSiteExistsReturnValue;
        }

        public IEnumerable<Model.Site> GetSitesFor(int customerId)
        {
            throw new NotImplementedException();
        }

        public ServiceResultDictionary Save(Model.Site site)
        {
            throw new NotImplementedException();
        }

        public Model.Site GetSite(int id)
        {
            throw new NotImplementedException();
        }

        public bool CustomerSiteExists(int customerId, string siteName)
        {
            return customerSiteExistsReturnValue;
        }

        public bool CustomerSiteExists(int customerId, string siteName, int siteIdToIgnore)
        {
            return customerSiteExistsReturnValue;
        }
    }
}
