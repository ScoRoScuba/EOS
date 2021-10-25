namespace EOS2.Web.BDD.Specs.ServiceProvider.Steps
{
    using System.Configuration;
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;
    using NUnit.Framework;
    using OpenQA.Selenium.IE;
    using TechTalk.SpecFlow;

    [Binding]
    public class CreateInstrumentSteps
    {
        [Given(@"I am on the '(.*)' page for customer '(.*)', site '(.*)' and plant area '(.*)'")]
        public void GivenIAmOnThePageForCustomerSiteAndPlantArea(string p0, string p1, string p2, string p3)
        {
        }

        [Given(@"The '(.*)' panel is displayed")]
        public void GivenThePanelIsDisplayed(string p0)
        {
        }

        [Given(@"I have selected '(.*)' from the '(.*)' dropdown")]
        public void GivenIHaveSelectedFromTheDropdown(string p0, string p1)
        {
        }

        [Given(@"'(.*)' is listed in the '(.*)' panel")]
        public void GivenIsListedInThePanel(string p0, string p1)
        {
        }

        [Given(@"'(.*)' has a '(.*)' button")]
        public void GivenHasAButton(string p0, string p1)
        {
        }

        [Then(@"I am on the '(.*)' page for plant area '(.*)'")]
        public void ThenIAmOnThePageForPlantArea(string p0, string p1)
        {
        }

        [Then(@"I am on the '(.*)' page for customer '(.*)', site '(.*)' and plant area '(.*)'")]
        public void ThenIAmOnThePageForCustomerSiteAndPlantArea(string p0, string p1, string p2, string p3)
        {
        }

        [Then(@"'(.*)' is listed in the '(.*)' panel")]
        public void ThenIsListedInThePanel(string p0, string p1)
        {
        }

        [Then(@"The '(.*)' dropdown is not displayed")]
        public void ThenTheDropdownIsNotDisplayed(string p0)
        {
        }

        [Then(@"The '(.*)' button is not displayed")]
        public void ThenTheButtonIsNotDisplayed(string p0)
        {
        }

        [Then(@"The '(.*)' dropdown is displayed")]
        public void ThenTheDropdownIsDisplayed(string p0)
        {
        }

        [Then(@"'(.*)' is not in the dropdown")]
        public void ThenIsNotInTheDropdown(string p0)
        {
        }

        [Then(@"The '(.*)' button is displayed")]
        public void ThenTheButtonIsDisplayed(string p0)
        {
        }

        [Then(@"'(.*)' is not listed in the '(.*)' panel")]
        public void ThenIsNotListedInThePanel(string p0, string p1)
        {
        }

        [Then(@"'(.*)' is listed in the '(.*)' dropdown")]
        public void ThenIsListedInTheDropdown(string p0, string p1)
        {
        }
    }
}
