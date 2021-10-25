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
    public class EditScheduleSteps
    {
        [Given(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)' and equipment '(.*)'")]
        public void GivenIAmOnThePageForCustomerSitePlantAreaAndEquipment(string p0, string p1, string p2, string p3, string p4)
        {
        }

        [Given(@"Schedule '(.*)' is listed")]
        public void GivenScheduleIsListed(string p0)
        {
        }

        [Given(@"Schedule '(.*)' has a '(.*)' button")]
        public void GivenScheduleHasAButton(string p0, string p1)
        {
        }

        [Given(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)', equipment '(.*)' and Schedule '(.*)'")]
        public void GivenIAmOnThePageForCustomerSitePlantAreaEquipmentAndSchedule(string p0, string p1, string p2, string p3, string p4, string p5)
        {
        }

        [When(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)', equipment '(.*)' and Schedule '(.*)'")]
        public void WhenIAmOnThePageForCustomerSitePlantAreaEquipmentAndSchedule(string p0, string p1, string p2, string p3, string p4, string p5)
        {
        }

        [Then(@"the '(.*)' radiobutton displays '(.*)'")]
        public void ThenTheRadioButtonDisplays(string p0, string p1)
        {
        }

        [Then(@"I am on the '(.*)' page for customer '(.*)', site '(.*)', plant area '(.*)', equipment '(.*)' and Schedule '(.*)'")]
        public void ThenIAmOnThePageForCustomerSitePlantAreaEquipmentAndSchedule(string p0, string p1, string p2, string p3, string p4, string p5)
        {
        }
    }
}
