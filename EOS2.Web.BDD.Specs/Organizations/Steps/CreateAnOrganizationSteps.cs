namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    [Binding]
    public class CreateAnOrganizationSteps
    {
        protected HomePage HomePage { get; set; }

        protected OrganizationsCustomerPage OrganizationsCustomersPage { get; set; }

        protected OrganizationsServiceProviderPage OrganizationsServiceProviderPage { get; set; }

        protected OrganizationsPortalAgentsIndexPage OrganizationsPortalAgentsIndexPage { get; set; }

        [BeforeScenario("CreateAnOrganization")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);
            OrganizationsCustomersPage = new OrganizationsCustomerPage(BeforeAfterTests.Driver);
        }

        [AfterScenario("CreateAnOrganization")]
        public void TearDown()
        {
            HomePage = null;
            OrganizationsCustomersPage = null;
            OrganizationsServiceProviderPage = null;
            OrganizationsPortalAgentsIndexPage = null;
        }

        [Given(@"I am on the Portal Agent Index Page")]
        public void GivenIAmOnThePortalAgentIndexPage()
        {
            this.OrganizationsPortalAgentsIndexPage = new OrganizationsPortalAgentsIndexPage(BeforeAfterTests.Driver);
            Assert.IsTrue(
                BeforeAfterTests.Driver.Title.Contains(
                    "Index - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("Portal Agents"));
        }

        [Given(@"I am on the Service Provider Index Page")]
        public void GivenIAmOnTheServiceProviderIndexPage()
        {
            this.OrganizationsServiceProviderPage = new OrganizationsServiceProviderPage(BeforeAfterTests.Driver);
        }

        [Then(@"I am on the Service Provider Organization Page")]
        public void ThenIAmOnTheServiceProviderOrganizationPage()
        {
            Assert.IsTrue(
                BeforeAfterTests.Driver.Title.Contains(
                    "Index - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("Service Providers"));
        }

        [When(@"I click on Add Service Provider button"), When(@"I click on Add Portal Agent button")]
        public void WhenIClickOnAddServiceProviderButton()
        {
            OrganizationsCustomersPage.ClickAddOrganization();
        }

        [Then(@"I am on the Create Service Provider Organization Page")]
        public void ThenIAmOnTheCreateServiceProviderOrganizationPage()
        {
            Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Edit - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("New Service Provider"));
        }
    }
}
