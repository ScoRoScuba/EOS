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
    public class CreateNewUserAccountSteps
    {
        protected HomePage HomePage { get; set; }

        protected AddOrganizationPage CustomersAddOrganizationPage { get; set; }

        protected OrganizationsUserPage OrganizationsUserPage { get; set; }

        [BeforeScenario("CreateNewUserAccount")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);
            CustomersAddOrganizationPage = new AddOrganizationPage(BeforeAfterTests.Driver, OrganizationType.Customer);
            OrganizationsUserPage = new OrganizationsUserPage(BeforeAfterTests.Driver);
        }

        [AfterScenario("CreateNewUserAccount")]
        public void TearDown()
        {
            HomePage = null;
            CustomersAddOrganizationPage = null;
            OrganizationsUserPage = null;
        }

        [Given(@"I am on the Organizations Home Page")]
        public void GivenIAmOnTheOrganizationsHomePage()
        {
            Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("title - Eurotherm Online Services Portal"));
        }

        [Given(@"I have the'(.*)'link")]
        public void GivenIHaveTheLink(string linkText)
        {
            Assert.IsTrue(CustomersAddOrganizationPage.ClickLinkIsExists(linkText));
        }

        [When(@"I click on the Add User Button")]
        public void WhenIClickOnTheAddUserButton()
        {
            OrganizationsUserPage.ClickAddUser();
        }

        [When(@"I set the User Name to '(.*)'")]
        public void WhenISetTheUserNameTo(string inputUserNameText)
        {
            OrganizationsUserPage.EnterUserName(inputUserNameText);
            FeatureContext.Current["UserName"] = inputUserNameText;
        }

        [When(@"I set the Name to '(.*)'")]
        public void WhenISetTheNameTo(string inputNameText)
        {
            OrganizationsUserPage.EnterName(inputNameText);
            FeatureContext.Current["New"] = inputNameText;
        }

        [When(@"I set the FamilyName to '(.*)'")]
        public void WhenISetTheFamilyNameTo(string inputFamilyNameText)
        {
            OrganizationsUserPage.EnterFamilyName(inputFamilyNameText);
            FeatureContext.Current["FamilyName"] = inputFamilyNameText;
        }

        [When(@"I set the Email Address to '(.*)'")]
        public void WhenISetTheEmailAddressTo(string inputEmailText)
        {
            OrganizationsUserPage.EnterEmailAddress(inputEmailText);
        }

        [When(@"I set the Comparison Email Address to '(.*)'")]
        public void WhenISetTheComparisonEmailAddressTo(string inputComparisonEmailAddressText)
        {
            OrganizationsUserPage.EnterConfirmationEmailAddress(inputComparisonEmailAddressText);
        }

        [Then(@"I am taken to the Organizations Page")]
        public void ThenIAmTakenToTheOrganizationsPage()
        {
            this.OrganizationsUserPage = new OrganizationsUserPage(BeforeAfterTests.Driver);
            Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("Eurotherm - EOS Owner Users - Eurotherm Online Services Portal"));
        }

        [Then(@"I am taken to the Organizations Page for (.*)")]
        public void ThenIAmTakenToTheOrganizationsPageForCustomer(string roleType)
        {
            switch (roleType)
            {
                case "Customer":
                    Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Eurotherm Customer - Customer Users - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("Eurotherm Customer - Customer Users"));
                    break;
                case "Service Provider":
                    Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Service Provider Organization - Service Provider Users - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("Service Provider Organization - Service Provider Users"));
                    break;
                default:
                    break;
            }
        }

        [Then(@"I am shown the New Users Page")]
        public void ThenIAmShownTheNewUsersPage()
        {
            Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("Eurotherm - EOS Owner - New User - Eurotherm Online Services Portal"));
        }

        [Then(@"I am shown the New Users Page for (.*)")]
        public void ThenIAmShownTheNewUsersPageForCustomer(string roleType)
        {
            switch (roleType)
            {
                case "Customer":
                    Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Eurotherm Customer - Customer - New User - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains("Eurotherm Customer - Customer - New User"));
                    break;

                case "Service Provider":
                    Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("Service Provider Organization - Service Provider - New User - Eurotherm Online Services Portal") && HomePage.GetHeaderTitle.Contains(" Service Provider Organization - Service Provider - New User"));
                    break;
                case "Portal Agents":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Portal Agent - New User - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Portal Agent - New User"));
                    break;
                case "EOS Owner":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Customer - New User - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Customer - New User"));
                    break;
                case "Service Provider User":
                case "Portal Agent Service Provider User":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Service Provider - New User - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Service Provider - New User"));
                    break;
                 
                default:
                    break;
            }
        }

        [Then(@"I am shown the message ""(.*)"" under the User Name"), Then(@"I am shown the message ""(.*)"" under the Name"), Then(@"I am shown the message '(.*)'"), Then(@"I am shown the message '(.*)' under the Name field")]
        public void ThenIAmShownTheMessageUnderTheUserName(string message)
        {
            Assert.IsTrue(OrganizationsUserPage.ValidationMessageTitle.Contains(message));
        }

        [Then(@"I am shown the message ""(.*)""")]
        public void ThenIAmShownTheMessage(string message)
        {
            Assert.IsTrue(OrganizationsUserPage.SuccessMessageTitle.Contains(message));
        }
    }
}
