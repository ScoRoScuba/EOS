namespace EOS2.Web.BDD.Specs.SiteOrganization.Steps
{
    using EOS2.Model;
    using EOS2.Web.BDD.Specs.Common;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;

    using TechTalk.SpecFlow;

    [Binding]
    public class CreatePlantAreaSteps
    {
        private Organization organization;

        protected HomePage HomePage { get; set; }

        [BeforeScenario("CreatePlantArea")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);
        }
        
        [Given(@"that I am signed in as the service provider '(.*)' with the password '(.*)'")]
        public void GivenThatIAmSignedInAsTheServiceProviderWithThePassword(string p0, string p1)
        {
            HomePage.SignIn(p0, p1);
        }

        [Given(@"that I have a customer organization '(.*)'")]
        public void GivenThatIHaveACustomerOrganization(string p0)
        {
            organization =
                OrganizationMaintenance.AddCustomer(
                    new Organization { Name = p0, Address = "Dummy Address", PostalCode = "BN13 3PL" });
        }

        [Given(@"that I have a site '(.*)'")]
        public void GivenThatIHaveASite(string p0)
        {
            SiteMaintenance.AddSite(
                organization,
                new Site
                    {
                        Name = p0,
                        Address = "Dummy Address",
                        PostalCode = "BN13 3PL",
                        OrganizationId = organization.Id
                    });
        }

        [When(@"I create the plant area '(.*)' description '(.*)'")]
        public void WhenICreateThePlantAreaDescription(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"'(.*)' site '(.*)' should list the plant area '(.*)'")]
        public void ThenSiteShouldListThePlantArea(string p0, string p1, string p2)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the '(.*)' field should show the error '(.*)'")]
        public void ThenTheFieldShouldShowTheError(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}