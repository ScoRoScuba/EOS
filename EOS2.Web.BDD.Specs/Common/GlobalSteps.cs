namespace EOS2.Web.BDD.Specs.Common
{
    using System;

    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;

    using TechTalk.SpecFlow;

    [Binding]
    public class GlobalSteps
    {
        protected HomePage HomePage { get; set; }

        [BeforeScenario]
        public void HomePageSetup() 
        {
            this.HomePage = new HomePage(BeforeAfterTests.Driver);
        }

        [Given(@"I am signed in as a '(.*)' '(.*)'")]
        public void GivenIAmSignedInAsA(string p0, string p1)
        {
            var organizationType = (OrganizationType)Enum.Parse(typeof(OrganizationType), p0);
            UserMaintenance.CreateUserAndRole(p0, p1, organizationType);
            this.HomePage.SignIn(p0);
        }
    }
}
