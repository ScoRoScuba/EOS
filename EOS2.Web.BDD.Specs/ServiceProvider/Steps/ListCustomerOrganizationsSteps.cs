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
    public class ListCustomerOrganizationsSteps
    {
        [Given(@"I am on the '(.*)' page")]
        public void GivenIAmOnThePage(string p0)
        {
        }

        [When(@"I press the '(.*)' link")]
        public void WhenIPressTheLink(string p0)
        {
        }

        [Then(@"I am on the '(.*)' page")]
        public void ThenIAmOnThePage(string p0)
        {
        }

        [Then(@"No customers are listed")]
        public void ThenNoCustomersAreListed()
        {
        }

        [Then(@"A '(.*)' with '(.*)' of '(.*)' is listed")]
        public void ThenAWithOfIsListed(string p0, string p1, string p2)
        {
        }

        [Given(@"'(.*)' Customers exist in the system")]
        public void GivenCustomersExistInTheSystem(int p0)
        {
        }
    }
}
