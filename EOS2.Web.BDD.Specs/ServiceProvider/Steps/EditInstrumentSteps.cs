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
    public class EditInstrumentSteps
    {
        [Given(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)' and instrument '(.*)'")]
        public void GivenIAmOnThePageForCustomerSitePlantAreaAndInstrument(string p0, string p1, string p2, string p3, string p4)
        {
        }

        [When(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)' and instrument '(.*)'")]
        public void WhenIAmOnThePageForCustomerSitePlantAreaAndInstrument(string p0, string p1, string p2, string p3, string p4)
        {
        }

        [When(@"I am shown the message ""(.*)""")]
        public void WhenIAmShownTheMessage(string p0)
        {
        }

        [Then(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)' and instrument '(.*)'")]
        public void ThenIAmOnThePageForCustomerSitePlantAreaAndInstrument(string p0, string p1, string p2, string p3, string p4)
        {
        }
    }
}
