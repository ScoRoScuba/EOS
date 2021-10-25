namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;
    using NUnit.Framework;
    using OpenQA.Selenium.IE;
    using TechTalk.SpecFlow;

    [Binding]
    public class CreateServiceProviderOrganizationSteps
    {
        protected HomePage HomePage { get; set; }

        protected OrganizationsUserPage OrganizationsUserPage { get; set; }

        [BeforeScenario("CreateServiceProviderOrganization")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);
        }

        [AfterScenario("CreateServiceProviderOrganization")]
        public void TearDown()
        {
            HomePage = null;
        }

        [Given(@"I am logged in as the Portal Agent '(.*)' with the password '(.*)'")]
        public void GivenIAmLoggedInAsThePortalAgentWithThePassword(string userName, string password)
        {
            HomePage.SignIn(userName, password);
        }
    }
}
