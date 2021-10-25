namespace EOS2.Web.BDD.Specs.ServiceProvider.Steps
{
    using System.Collections.Generic;

    using EOS2.Model;

    using TechTalk.SpecFlow;

    [Binding]
    public class ViewPlantAreaSteps
    {
        // TODO: If we keep this way of navigating then move to a global place and pass pageName as an Enum
        private void NavigateTo(string pageName, Dictionary<string, int> criteria)
        {
            // TODO: this is just a placeholder as not sure what we are doing with pageObjects at the moment
            ScenarioContext.Current.Pending();
        }

        [Given(@"A Plant Area exists")]
        public void GivenAPlantAreaExists()
        {
            var plantArea = SharedSteps.SinglePlantAreaSetup();
            ScenarioContext.Current["PlantArea"] = plantArea;
        }

        [When(@"I go to the Site Details page")]
        public void WhenIGoToTheSiteDetailsPage()
        {
            var plantArea = (PlantArea)ScenarioContext.Current["PlantArea"];
            var criteria = new Dictionary<string, int>
                               {
                                   { "plantAreaId", plantArea.Id },
                                   { "siteId", plantArea.Site.Id },
                                   { "customerId", plantArea.Site.Customer.Id }
                               };
            this.NavigateTo("PlantAreaDetails", criteria);
            ScenarioContext.Current.Pending();
        }

        [Then(@"A Plant Area is listed")]
        public void ThenAPlantAreaIsListed()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I go to the Site Details page")]
        public void GivenIGoToTheSiteDetailsPage()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"A Plant Area is listed with a '(.*)' button")]
        public void GivenAPlantAreaIsListedWithAButton(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I am on the Plant Area Details page")]
        public void ThenIAmOnThePlantAreaDetailsPage()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I go to the Plant Area Details page")]
        public void GivenIGoToThePlantAreaDetailsPage()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I am shown the correct data")]
        public void ThenIAmShownTheCorrectData()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
