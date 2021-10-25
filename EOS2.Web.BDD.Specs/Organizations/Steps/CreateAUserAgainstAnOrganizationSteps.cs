namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using System;
    using System.Globalization;

    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    [Binding]
    public class CreateAUserAgainstAnOrganizationSteps
    {
        protected AddOrganizationPage AddOrganizationPage { get; set; }

        protected HomePage HomePage { get; set; }

        protected SetPasswordPage SetPasswordPage { get; set; }

        protected SignInPage SignInPage { get; set; }

        [BeforeScenario("CreateAUserAgainstAnOrganization")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);
            AddOrganizationPage = new AddOrganizationPage(BeforeAfterTests.Driver, OrganizationType.Customer);
            SetPasswordPage = new SetPasswordPage(BeforeAfterTests.Driver);
            SignInPage = new SignInPage(BeforeAfterTests.Driver);
        }

        [AfterScenario("CreateAUserAgainstAnOrganization")]
        public void TearDown()
        {
            HomePage = null;
            AddOrganizationPage = null;
            SetPasswordPage = null;
            SignInPage = null;
        }

        [Given(@"I am on the EOS Owners Organizations Home Page")]
        public void GivenIAmOnTheEOSOwnersOrganizationsHomePage()
        {
            Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("title - Eurotherm Online Services Portal"));
        }

        [Given(@"I am on the Portal Agents Organizations Home Page")]
        public void GivenIAmOnThePortalAgentsOrganizationsHomePage()
        {
            Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("title - Eurotherm Online Services Portal"));
        }

        [Given(@"I am on the Service Providers Organizations Home Page")]
        public void GivenIAmOnTheServiceProvidersOrganizationsHomePage()
        {
            Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("title - Eurotherm Online Services Portal"));
        }

        [When(@"I click on the '(.*)' button for '(.*)'")]
        public void WhenIClickOnTheButtonFor(string buttonName, string agentName)
        {
            var agentDetails = AddOrganizationPage.GetPortalAgentsDetails;
            FeatureContext.Current["agentName"] = agentName;
            if (agentDetails.Count > 0)
            {
                for (int i = 0; i < agentDetails.Count; i++)
                {
                    if (agentDetails[i].Text.Contains(agentName) && agentDetails[i].Text.Contains("Manage Users"))
                    {
                        FeatureContext.Current["ManageUserButton"] = i;
                        AddOrganizationPage.ClickManageUserButton(i);
                        break;
                    }
                }
            }
            else
            {
                Assert.IsTrue(agentDetails.Count > 0);
            }
        }

        [When(@"I click the '(.*)'")]
        public void WhenIClickThe(string p0)
        {
            AddOrganizationPage.ClickManageUserButton(Convert.ToInt32(FeatureContext.Current["setPassword"], CultureInfo.InvariantCulture));
        }

        [When(@"set the Confirmation Password to '(.*)'")]
        public void WhenSetTheConfirmationPasswordTo(string inputConfirmationPassword)
        {
            SetPasswordPage.EnterConfirmationPassword(inputConfirmationPassword);
        }

        [When(@"I click on '(.*)'")]
        public void WhenIClickOn(string link)
        {
            switch (link)
            {
                case "Sign In":
                    SignInPage.SignIn();
                    break;
                case "Sign Out":
                    SignInPage.SignOut();
                    break;
                default:
                    break;
            }
        }

        [When(@"I set the Password to '(.*)'")]
        public void WhenISetThePasswordTo(string inputPassword)
        {
            SetPasswordPage.EnterPassword(inputPassword);
        }

        [Then(@"I am shown a list of (.*)")]
        public void ThenIAmShownAListOfPortalAgents(string role)
        {
            var agentDetails = AddOrganizationPage.GetPortalAgentsDetails;
            Assert.IsTrue(agentDetails.Count > 0);
        }

        [Then(@"I am shown '(.*)' Page")]
        public void ThenIAmShownPage(string navigationPage)
        {
            switch (navigationPage)
            {
                case "Portal Agents Users":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " "
                            + "Portal Agent Users - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(
                            FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Portal Agent Users"));
                    break;
                case "New Customer Organization Users":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Customer Users - Eurotherm Online Services Portal") &&
                        HomePage.GetHeaderTitle.Contains(FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Customer Users"));
                    break;
                case "New Service Provider Organization Users":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Service Provider Users - Eurotherm Online Services Portal") 
                        && HomePage.GetHeaderTitle.Contains(FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Service Provider Users"));
                    break;
                case "New Portal Agent Service Provider Organization Users":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Service Provider Users - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(FeatureContext.Current["agentName"].ToString() + " " + "-" + " " + "Service Provider Users"));
                    break;
            }
        }

        [Then(@"I see the new User in the list")]
        public void ThenISeeTheNewUserInTheList()
        {
            var agentDetails = AddOrganizationPage.GetPortalAgentsDetails;
            bool iscreatedUserExists = false;
            if (agentDetails.Count > 0)
            {
                for (int j = 0; j < agentDetails.Count; j++)
                {
                    if (agentDetails[j].Text.Contains(FeatureContext.Current["UserName"].ToString()))
                    {
                        FeatureContext.Current["setPassword"] = j;
                        iscreatedUserExists = true;
                        break;
                    }
                }

                Assert.IsTrue(iscreatedUserExists);
            }
            else
            {
            Assert.IsTrue(agentDetails.Count > 0);
            }
        }

        [Then(@"I see the button '(.*)'")]
        public void ThenISeeTheButton(string p0)
        {
            Assert.IsTrue(AddOrganizationPage.ClickSetPasswordButtonExists(Convert.ToInt32(FeatureContext.Current["setPassword"], CultureInfo.InvariantCulture)));
        }

        [Then(@"I am taken to the Set Password Page")]
        public void ThenIAmTakenToTheSetPasswordPage()
        {
            Assert.IsTrue(
                BeforeAfterTests.Driver.Title.Contains(
                    "Set Password - Eurotherm Online Services Portal")
                    && HomePage.GetHeaderTitle.Contains(FeatureContext.Current["FamilyName"].ToString()));
        }

        [Then(@"the UserName is set to '(.*)'")]
        public void ThenTheUserNameIsSetTo(string p0)
        {
            Assert.IsTrue(HomePage.GetHeaderTitle.Contains(FeatureContext.Current["New"].ToString()));
        }

        [Then(@"I am shown the '(.*)'")]
        public void ThenIAmShownThe(string p0)
        {
            Assert.IsTrue(
                BeforeAfterTests.Driver.Title.Contains("title - Eurotherm Online Services Portal") && SignInPage.IsSignInLinkExists);
        }

        [Then(@"I am shown the Portal Agents Organizations Home Page")]
        public void ThenIAmShownThePortalAgentsOrganizationsHomePage()
        {
            Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("title - Eurotherm Online Services Portal") && AddOrganizationPage.ClickLinkIsExists("Service Providers"));
        }

        [Then(@"I am shown the Service Providers Organizations Home Page")]
        public void ThenIAmShownTheServiceProvidersOrganizationsHomePage()
        {
            Assert.IsTrue(
                BeforeAfterTests.Driver.Title.Contains(
                    "title - Eurotherm Online Services Portal")
                    && AddOrganizationPage.ClickLinkIsExists("Users")
                    && AddOrganizationPage.ClickLinkIsExists("Customers"));
        }

        [Then(@"I am shown the Customer Organizations Home Page for '(.*)'"), Then(@"I am shown the Customers Organizations Home Page")]
        public void ThenIAmShownTheCustomerOrganizationsHomePageFor(string p0)
        {
            Assert.IsTrue(BeforeAfterTests.Driver.Title.Contains("title - Eurotherm Online Services Portal") && AddOrganizationPage.ClickLinkIsExists("Users"));
        }
    }
}
