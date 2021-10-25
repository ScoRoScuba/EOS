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
    public class ViewInstrumentsSteps
    {
        [Given(@"Instrument '(.*)' is listed")]
        public void GivenInstrumentIsListed(string p0)
        {
        }

        [Given(@"Instrument '(.*)' has a '(.*)' button")]
        public void GivenInstrumentHasAButton(string p0, string p1)
        {
        }

        [Then(@"the '(.*)' dropdown displays '(.*)'")]
        public void ThenTheDropdownDisplays(string p0, string p1)
        {
        }
    }
}
