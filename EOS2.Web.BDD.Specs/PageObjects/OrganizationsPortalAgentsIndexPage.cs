namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class OrganizationsPortalAgentsIndexPage : BasePageObject
    {
        [FindsBy(How = How.CssSelector, Using = "#add-organization")]
        private readonly IWebElement addButton = null;

        public OrganizationsPortalAgentsIndexPage(IWebDriver driver)
            : base(driver, new Uri("/Organizations/PortalAgent", UriKind.Relative))
        {
        }

        public void ClickAddOrganization()
        {
            addButton.Click();
        }
    }
}
