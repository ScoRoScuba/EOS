namespace EOS2.Services.Tests.SiteServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Services.BusinessDomain;

    using Moq;
    using NUnit.Framework;

    public abstract class SiteServiceTestsBase
    {
        protected Mock<IRepository<Site>> MockSiteRepository { get; set; }
 
        [SetUp]
        public void FixtureSetup()
        {
            MockSiteRepository = new Mock<IRepository<Site>>();
        }

        public ISiteService ServiceUnderTest()
        {
            return new SiteService(MockSiteRepository.Object);
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class GetCustomerSitesMethod : SiteServiceTestsBase
    {           
        [Test]
        public void GetCustomerSitesFromRepository()
        {
            // ARRANGE
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1, OrganizationId = 1, Name = "Should have site 1" },
                                    new Site() { Id = 2, OrganizationId = 1, Name = "Should have site 2" },
                                    new Site() { Id = 3, OrganizationId = 2 },
                                    new Site() { Id = 4, OrganizationId = 2 },
                                    new Site() { Id = 5, OrganizationId = 2 },
                                    new Site() { Id = 6, OrganizationId = 1, Name = "Should have site 6" },
                                    new Site() { Id = 7, OrganizationId = 1, Name = "Should have site 7" },
                                    new Site() { Id = 8, OrganizationId = 3 },
                                    new Site() { Id = 9, OrganizationId = 3 },
                                    new Site() { Id = 10, OrganizationId = 1, Name = "Should have site 10" },
                                };

            IList<Site> filteredResults = null;
           
            MockSiteRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => filteredResults = siteList.AsQueryable().Where(pred).ToList()).Returns(() => filteredResults);

            const int CustomerIdToUse = 1;

            var siteService = this.ServiceUnderTest();

            // ACT
            var results = siteService.GetSitesFor(CustomerIdToUse);

            // ASSERT                
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Any(s => s.OrganizationId != 1), Is.False);
            Assert.That(results.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ReturnsEmptyListOfSitesForUnknownCustomer()
        {
            const int CustomerId = 1;
            const int UnknownCustomerId = 2;

            var siteList = new List<Site>()
                                {
                                    new Site() { OrganizationId = CustomerId },
                                    new Site() { OrganizationId = CustomerId },
                                    new Site()
                                };

            MockSiteRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<Site, bool>>>())).Returns(siteList.Where(s => s.OrganizationId == UnknownCustomerId).ToList());

            ISiteService siteService = new SiteService(MockSiteRepository.Object);

            var resultList = siteService.GetSitesFor(UnknownCustomerId);
                
            Assert.That(resultList, Is.Empty);
        }
    }

    [TestFixture]
    public class SaveSiteMethod : SiteServiceTestsBase
    {
        [Test]
        public void AddsSiteToRepository()
        {           
            var siteList = new List<Site>();
            var siteToAdd = new Site();

            MockSiteRepository.Setup(a => a.Add(It.IsAny<Site>())).Callback((Site site) => siteList.Add(site));

            MockSiteRepository.Setup(a => a.GetAll()).Returns(siteList);

            var siteService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            siteService.Save(siteToAdd);

            // Assert ( Test its worked )
            Assert.That(siteList, Is.Not.Empty);
        }

        [Test]
        public void SaveSiteToRepositoryWithCorrectCustomerId()
        {
            const int CustomerId = 1;
         
            var siteList = new List<Site>();
            var siteToAdd = new Site() { OrganizationId = CustomerId };

            MockSiteRepository.Setup(a => a.Add(It.IsAny<Site>())).Callback((Site site) => siteList.Add(site));

            MockSiteRepository.Setup(a => a.GetAll()).Returns(siteList);

            var siteService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            siteService.Save(siteToAdd);

            Assert.That(siteList.Count(), Is.EqualTo(1));
            Assert.That(siteList.Count(s => s.OrganizationId == CustomerId), Is.EqualTo(1));
        }

        [Test]
        public void SaveSiteUpdatesExistingSite()
        {
            const int CustomerId = 1;
          
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1, OrganizationId = CustomerId, Name = "Site one", PostalCode = "postal code" },
                                    new Site() { Id = 2, OrganizationId = CustomerId, Name = "Site one", PostalCode = "postal code" }
                                };

            MockSiteRepository.Setup(a => a.Update(It.IsAny<Site>())).Callback(
                (Site site) =>
                    {
                        siteList.Single(s => s.Id == site.Id).Name = site.Name;
                    });

            var siteService = this.ServiceUnderTest();

            var siteToUpdate = new Site() { Id = 1, OrganizationId = CustomerId, Name = "Site one updated", PostalCode = "postal code" };

            // ACT ( Run the actual Test )
            siteService.Save(siteToUpdate);

            Assert.That(siteList.Single(s => s.Id == 1).Name, Is.EqualTo(siteToUpdate.Name));
        }
    }

    [TestFixture]
    public class GetSiteMethod : SiteServiceTestsBase
    {
        [Test]
        public void ReturnsFoundSite()
        {
            // ARRANGE
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1 },
                                    new Site() { Id = 2 },
                                };

            Site foundSite = null;
                
            MockSiteRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => foundSite = siteList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSite);

            const int SiteIdToUse = 1;
            var siteService = this.ServiceUnderTest();                

            // ACT 
            var result = siteService.GetSite(SiteIdToUse);
                
            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsNullWhenNoSiteFound()
        {
            // ARRANGE
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1 },
                                    new Site() { Id = 2 },
                                };

            Site foundSite = null;
            
            MockSiteRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => foundSite = siteList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSite);

            const int SiteIdToUse = 3;
            var siteService = this.ServiceUnderTest();

            // ACT 
            var result = siteService.GetSite(SiteIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);                            
        }
    }

    [TestFixture]
    public class CustomerSiteExists : SiteServiceTestsBase
    {            
        [Test]
        public void ReturnsTrueIfAddingCustomerSiteFound()
        {
            // ARRANGE
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1, OrganizationId = 1, Name = "Test Site 1" },
                                    new Site() { Id = 2, OrganizationId = 2, Name = "Test Site 2" },
                                };

            Site foundSite = null;
            
            MockSiteRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => foundSite = siteList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSite);

            const int CustomerIdToUse = 1;
            const string SiteNameToFind = "Test Site 1";

            var siteService = this.ServiceUnderTest();

            // ACT 
            var result = siteService.CustomerSiteExists(CustomerIdToUse, SiteNameToFind);

            // ASSERT
            Assert.That(result, Is.True);                            
        }

        [Test]
        public void ReturnsFalseIfAddingCustomerSiteNotFound()
        {
            // ARRANGE
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1, OrganizationId = 1, Name = "Test Site 1" },
                                    new Site() { Id = 2, OrganizationId = 2, Name = "Test Site 2" },
                                };

            Site foundSite = null;
            
            MockSiteRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => foundSite = siteList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSite);

            const int CustomerIdToUse = 2;
            const string SiteNameToFind = "Test Site 1";

            var siteService = this.ServiceUnderTest();

            // ACT 
            var result = siteService.CustomerSiteExists(CustomerIdToUse, SiteNameToFind);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsTrueIfUpdatingSiteFound()
        {
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1, OrganizationId = 1, Name = "Test Site 1" },
                                    new Site() { Id = 2, OrganizationId = 1, Name = "Test Site 2" },
                                    new Site() { Id = 3, OrganizationId = 2, Name = "Test Site 1" },
                                    new Site() { Id = 4, OrganizationId = 2, Name = "Test Site 2" }
                                };

            Site foundSite = null;
            
            MockSiteRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => foundSite = siteList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSite);

            const int CustomerIdToUse = 2;
            const int CustomerSiteIdToIgnore = 2;
            const string SiteNameToFind = "Test Site 1";

            var siteService = this.ServiceUnderTest();

            // ACT 
            var result = siteService.CustomerSiteExists(CustomerIdToUse, SiteNameToFind, CustomerSiteIdToIgnore);

            // ASSERT
            Assert.That(result, Is.True);                   
        }

        [Test]
        public void ReturnsFalseIfUpdatingSiteNotFound()
        {
            var siteList = new List<Site>()
                                {
                                    new Site() { Id = 1, OrganizationId = 1, Name = "Test Site 1" },
                                    new Site() { Id = 2, OrganizationId = 1, Name = "Test Site 2" },
                                    new Site() { Id = 3, OrganizationId = 2, Name = "Test Site 1" },
                                    new Site() { Id = 4, OrganizationId = 2, Name = "Test Site 2" }
                                };

            Site foundSite = null;

            MockSiteRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Site, bool>>>()))
                .Callback(
                    (Expression<Func<Site, bool>> pred) => foundSite = siteList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSite);

            const int CustomerIdToUse = 2;
            const int CustomerSiteIdToIgnore = 2;
            const string SiteNameToFind = "Test Site 3";

            var siteService = this.ServiceUnderTest();

            // ACT 
            var result = siteService.CustomerSiteExists(CustomerIdToUse, SiteNameToFind, CustomerSiteIdToIgnore);

            // ASSERT
            Assert.That(result, Is.False);
        }
    }
}
