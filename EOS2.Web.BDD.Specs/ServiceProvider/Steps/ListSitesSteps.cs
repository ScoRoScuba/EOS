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
    public class ListSitesSteps
    {
        [When(@"I press the '(.*)' button for '(.*)'")]
        public void WhenIPressTheButtonFor(string p0, string p1)
        {
        }

        [Then(@"No '(.*)' are listed")]
        public void ThenNoAreListed(string p0)
        {
        }
    }
}
