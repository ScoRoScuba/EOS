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
    public class CreateSiteSteps
    {
        [Given(@"I am on the '(.*)' page for '(.*)'")]
        public void GivenIAmOnThePageFor(string p0, string p1)
        {
        }
    }
}
