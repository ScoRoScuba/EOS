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
    public class CreateScheduleSteps
    {
        [Given(@"I am on the Equipments page")]
        public void GivenIAmOnTheEquipmentsPage()
        {
        }

        [Given(@"the add button text is '(.*)'")]
        public void GivenTheAddButtonTextIs(string p0)
        {
        }

        [When(@"I click the Add Schedule button")]
        public void WhenIClickTheAddScheduleButton()
        {
        }

        [Then(@"the New Schedule page is displayed'")]
        public void ThenTheNewSchedulePageIsDisplayed()
        {
        }

        [Then(@"the '(.*)' page is displayed")]
        public void ThenThePageIsDisplayed(string p0)
        {
        }

        [Given(@"I have selected '(.*)' in the '(.*)' dropdown")]
        public void GivenIHaveSelectedInTheDropdown(string p0, string p1)
        {
        }

        [Given(@"I have selected '(.*)' in the '(.*)' radio button")]
        public void GivenIHaveSelectedInTheRadioButton(string p0, string p1)
        {
        }
    }
}
