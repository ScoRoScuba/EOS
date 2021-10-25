namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using System.Configuration;

    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class CreatePortalAgentOrganizationSteps
    {
        protected HomePage HomePage { get; set; }

        protected OrganizationsPortalAgentsIndexPage PortalAgentIndexPage { get; set; }

        protected AddOrganizationPage PortalAgentAddPage { get; set; }

        [BeforeScenario("CreatePortalAgentOrganization")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);            
        }

        [AfterScenario("CreatePortalAgentOrganization")]
        public void TearDown()
        {
            HomePage = null;
        }

        [Given(@"I am logged in as the EOS Owner '(.*)' with the password '(.*)'"), When(@"I am logged in as the Portal Agent User '(.*)' with the password '(.*)'"), When(@"I am logged in as the Service Provider User '(.*)' with the password '(.*)'"), When(@"I am logged in as the Customer User '(.*)' with the password '(.*)'"), When(@"I am logged in as the Service Provider Cutomer User '(.*)' with the password '(.*)'")]
        public void GivenIAmLoggedInAsTheEOSOwnerWithThePassword(string p0, string p1)
        {
            HomePage.SignIn(p0, p1);            
        }

        [Then(@"I am on the Portal Agent Organization Page")]
        public void ThenIAmOnThePortalAgentOrganizationPage()
        {
            this.PortalAgentIndexPage = new OrganizationsPortalAgentsIndexPage(BeforeAfterTests.Driver);
            Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Index - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("Portal Agents"));
        }

        [Then(@"I am on the Create Portal Agent Organization Page")]
        public void ThenIAmOnTheCreatePortalAgentOrganizationPage()
        {
            Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Edit - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("New Portal Agent"));
        }

        [When(@"I enter Portal Agent Name to '(.*)'")]
        public void WhenIEnterPortalAgentNameTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I enter the Address of '(.*)'")]
        public void WhenIEnterTheAddressOf(string p0)
        {
        }

        [When(@"I enter a Postal Code of '(.*)'")]
        public void WhenIEnterAPostalCodeOf(string p0)
        {
        }
    }
}
