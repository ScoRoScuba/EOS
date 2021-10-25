namespace EOS2.Web.BDD.Specs.Organizations.Steps
{
    using System;
    using System.Globalization;

    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.IE;

    using TechTalk.SpecFlow;

    [Binding]
    public class ViewOrganizationDetailsSteps
    {
        protected HomePage HomePage { get; set; }

        protected OrganizationsUserPage OrganizationsUserPage { get; set; }

        protected AddOrganizationPage AddOrganizationPage { get; set; }

        [BeforeScenario("ViewOrganizationDetails")]
        public void SetUp()
        {
            DatabaseMaintenance.Reset();

            HomePage = new HomePage(BeforeAfterTests.Driver);
            AddOrganizationPage = new AddOrganizationPage(BeforeAfterTests.Driver, OrganizationType.Customer);
        }

        [AfterScenario("ViewOrganizationDetails")]
        public void TearDown()
        {
            HomePage = null;
            AddOrganizationPage = null;
        }

        [Given(@"Portal Agent '(.*)' is listed")]
        public void GivenPortalAgentIsListed(string agentName)
        {
            var agentDetails = AddOrganizationPage.GetPortalAgentsDetails;
            FeatureContext.Current["agentName"] = agentName;
            bool agentDetailsExists = false;
            if (agentDetails.Count > 0)
            {
                for (int i = 0; i < agentDetails.Count; i++)
                {
                    if (agentDetails[i].Text.Contains(agentName) && agentDetails[i].Text.Contains("Details"))
                    {
                        agentDetailsExists = true;
                        FeatureContext.Current["detailsButton"] = i;
                        FeatureContext.Current["detailsButtonExists"] = agentDetailsExists;
                        break;
                    }
                }

                Assert.IsTrue(agentDetailsExists);
            }
            else
            {
                Assert.IsTrue(agentDetails.Count > 0);
            }
        }

        [Given(@"Portal Agent '(.*)' has a '(.*)' button")]
        public void GivenPortalAgentHasAButton(string agentName, string detailsButton)
        {
            Assert.IsTrue(Convert.ToBoolean(FeatureContext.Current["detailsButtonExists"], CultureInfo.InvariantCulture));
        }

        [When(@"I press the '(.*)' button")]
        public void WhenIPressTheButton(string p0)
        {
            AddOrganizationPage.ClickDetailsButton(Convert.ToInt32(FeatureContext.Current["detailsButton"], CultureInfo.InvariantCulture));
        }

        [Then(@"I am shown the '(.*)' page")]
        public void ThenIAmShownThePage(string roleTypes)
        {
            switch (roleTypes)
            {
                case "Portal Agent Details":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains("Edit - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains("Portal Agent Details -" + " " + FeatureContext.Current["agentName"]));
                    break;

                case "Service Provider Details":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains("Edit - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains("Service Provider Details -" + " " + FeatureContext.Current["agentName"]));
                    break;

                case "Customer Details":
                     Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains("Edit - Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains("Customer Details -" + " " + FeatureContext.Current["agentName"]));
                    break;

                case "User Details":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(
                            FeatureContext.Current["agentName"] + " " + "-" + " "
                            + "Portal Agent - User Details -" + " " + FeatureContext.Current["New"] + " " + "-"
                            + " " + "Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(FeatureContext.Current["New"].ToString()));
                    break;

                case"Sign In":
                    Assert.That(BeforeAfterTests.Driver.Title, Is.EqualTo("Sign In - Eurotherm Online Services Portal"));
                    break;

                case "User Details For Customer":
                case "Service Provider Customer User Details":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(FeatureContext.Current["agentName"] + " " + "-" + " " + "Customer - User Details -" + " " + FeatureContext.Current["New"].ToString() + " " + "-" + " " + "Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(FeatureContext.Current["New"].ToString()));
                    break;
                case "User Details For Service Provider":
                    Assert.IsTrue(
                        BeforeAfterTests.Driver.Title.Contains(FeatureContext.Current["agentName"] + " " + "-" + " " + "Service Provider - User Details -" + " " + FeatureContext.Current["New"].ToString() + " " + "-" + " " + "Eurotherm Online Services Portal")
                        && HomePage.GetHeaderTitle.Contains(FeatureContext.Current["New"].ToString()));
                    break;
            }
        }

        [Then(@"the '(.*)' textbox displays '(.*)'")]
        public void ThenTheTextboxDisplays(string textBox, string textBoxValue)
        {
            switch (textBox)
            {
                case "Name":
                    Assert.IsTrue(AddOrganizationPage.GetName.Contains(textBoxValue));
                    break;
                case "Address":
                    Assert.IsTrue(AddOrganizationPage.GetAddress.Contains(textBoxValue));
                    break;
                case "ZIP Code":
                    Assert.IsTrue(AddOrganizationPage.GetPostalCode.Contains(textBoxValue));
                    break;
                default:
                    break;
            }
        }
    }
}
