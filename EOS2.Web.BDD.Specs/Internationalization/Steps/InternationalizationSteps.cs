namespace EOS2.Web.BDD.Specs.Internationalization.Steps
{
    using System.Configuration;
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;
    using NUnit.Framework;
    using OpenQA.Selenium.IE;
    using TechTalk.SpecFlow;

    [Binding]
    public class InternationalizationSteps
    {
        protected HomePage HomePage { get; set; }
        
        protected OrganizationsCustomerPage OrganizationsCustomersPage { get; set; }

        protected AddOrganizationPage CustomersAddOrganizationPage { get; set; }

        [BeforeScenario("Internationalization")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            this.HomePage = new HomePage(BeforeAfterTests.Driver);
            CustomersAddOrganizationPage = new AddOrganizationPage(BeforeAfterTests.Driver, OrganizationType.Customer);
        }

        [AfterScenario("Internationalization")]
        public void TearDown()
        {
            this.HomePage = null;
            CustomersAddOrganizationPage = null;
        }

        [Given(@"my browser is set to prefer '(.*)'")]
        public void GivenMyBrowserIsSetToPrefer(string p0)
        {
            if (BeforeAfterTests.Driver.GetType() == typeof(InternetExplorerDriver))
            {
                // This test cannot be done with IE, its language settings come from the operating system.
                ScenarioContext.Current.Pending();
            }
            else
            {
                this.HomePage.SetBrowserLanguagePreference(p0);
            }
        }

        [When(@"EOS language selection is '(.*)'")]
        public void WhenEOSLanguageSelectionIs(string p0)
        {
            this.HomePage.ChooseLanguage(p0);
        }

        [Given(@"EOS language selection is '(.*)'")]
        public void GivenEOSLanguageSelectionIs(string p0)
        {
            this.WhenEOSLanguageSelectionIs(p0);
        }

        [Given(@"the Eurotherm Online Service title is not surrounded by vertical bars")]
        public void GivenTheEurothermOnlineServiceTitleIsNotSurroundedByVerticalBars()
        {
            Assert.That(this.HomePage.PageTitle, Is.EqualTo("Eurotherm Online Services"));
        }

        [Given(@"I am signed in as '(.*)' with the password '(.*)'")]
        public void GivenIAmSignedInAsWithThePassword(string p0, string p1)
        {
           this.HomePage.SignIn(p0, p1);
        }

        [Given(@"I am on the Customer Organizations page")]
        public void GivenIAmOnTheCustomerOrganizationsPage()
        {
            this.OrganizationsCustomersPage = new OrganizationsCustomerPage(BeforeAfterTests.Driver);
        }

        [Given(@"the organization table heading is '(.*)'")]
        public void GivenTheOrganizationTableHeadingIs(string p0)
        {
            Assert.That(this.OrganizationsCustomersPage.OrganizationsTableHeadingText, Is.EqualTo(p0));
        }

        [When(@"I choose '(.*)' from the language selector and click Ok")]
        public void WhenIChooseFromTheLanguageSelectorAndClickOk(string p0)
        {
            this.HomePage.ChooseLanguage(p0);
        }

        [When(@"I choose '(.*)' from the language selector on the Customer Organizations page and click Ok")]
        public void WhenIChooseFromTheLanguageSelectorOnTheCustomerOrganizationsPageAndClickOk(string p0)
        {
            this.OrganizationsCustomersPage.ChooseLanguage(p0);
        }

        [When(@"I click the Add Customer Organization button")]
        public void WhenIClickTheAddCustomerOrganizationButton()
        {
            this.OrganizationsCustomersPage.ClickAddOrganization();
            Assert.That(BeforeAfterTests.Driver.Url, Is.EqualTo(ConfigurationManager.AppSettings["seleniumBaseUrl"] + "/Organizations/Customer/New"));
            this.CustomersAddOrganizationPage = new AddOrganizationPage(BeforeAfterTests.Driver, OrganizationType.Customer);
        }

        [When(@"I set the name to '(.*)' and the address to '(.*)' and the Postal Code to '(.*)'")]
        public void WhenISetTheNameToAndTheAddressToAndThePostalCodeTo(string p0, string p1, string p2)
        {
            this.CustomersAddOrganizationPage.FillInForm(p0, p1, p2);
        }

        [Then(@"I enter the Address of '(.*)'")]
        public void ThenIEnterTheAddressOf(string organizationAddress)
        {
            this.CustomersAddOrganizationPage.SetOrganizationAddress(organizationAddress);
        }

        [Then(@"I enter a Postal Code of '(.*)'")]
        public void ThenIEnterAPostalCodeOf(string organizationPostalCode)
        {
            this.CustomersAddOrganizationPage.SetOrganizationPostalCode(organizationPostalCode);
        }

        [When(@"I click the Save Button")]
        public void WhenIClickTheSaveButton()
        {
            this.CustomersAddOrganizationPage.ClickSave();
        }

        [When(@"I click the '(.*)' button")]
        public void WhenIClickTheButton(string p0)
        {
            this.CustomersAddOrganizationPage.ClickCancel();
        }

        [When(@"I click the'(.*)'link"), When(@"I click on the '(.*)' menu option")]
        public void WhenIClickTheLink(string optionsLink)
        {
            this.CustomersAddOrganizationPage.ClickLink(optionsLink);
        }

        [Then(@"the Eurotherm Online Services title should be surrounded by vertical bars")]
        public void ThenTheEurothermOnlineServicesTitleShouldBeSurroundedByVerticalBars()
        {
            Assert.That(this.HomePage.PageTitle, Is.EqualTo("|Eurotherm Online Services|"));
        }

        [Then(@"the organization table heading is '(.*)'")]
        public void ThenTheOrganizationTableHeadingIs(string p0)
        {
            Assert.That(this.OrganizationsCustomersPage.OrganizationsTableHeadingText, Is.EqualTo(p0));
        }
    }
}
