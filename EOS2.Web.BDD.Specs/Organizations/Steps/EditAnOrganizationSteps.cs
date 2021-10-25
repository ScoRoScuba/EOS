namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using System.Configuration;
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;
    using NUnit.Framework;
    using OpenQA.Selenium.IE;
    using TechTalk.SpecFlow;

    [Binding]
    public class EditAnOrganizationSteps
    {
        protected AddOrganizationPage AddOrganizationPage { get; set; }

        [BeforeScenario("EditAnOrganization")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            AddOrganizationPage = new AddOrganizationPage(BeforeAfterTests.Driver, OrganizationType.Customer);
        }

        [AfterScenario("EditAnOrganization")]
        public void TearDown()
        {
            AddOrganizationPage = null;
        }

        [When(@"I change the '(.*)' textbox to '(.*)'")]
        public void WhenIChangeTheTextboxTo(string p0, string organizationAddress)
        {
            this.AddOrganizationPage.SetOrganizationAddress(organizationAddress);
        }
    }
}
