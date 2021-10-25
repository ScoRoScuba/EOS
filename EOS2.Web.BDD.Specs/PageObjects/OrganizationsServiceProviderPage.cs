namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    public class OrganizationsServiceProviderPage : BasePageObject
    {
        public OrganizationsServiceProviderPage(IWebDriver driver)
           : base(driver, new Uri("/Organizations/ServiceProvider", UriKind.Relative))
        {
        }
    }
}
