namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    public class SetPasswordPage : BasePageObject
    {
        [FindsBy(How = How.Id, Using = "Password")]
        private readonly IWebElement password = null;

        [FindsBy(How = How.Id, Using = "ConfirmationPassword")]
        private readonly IWebElement confirmationPassword = null;

        public SetPasswordPage(IWebDriver driver)
            : base(driver, new Uri("/Organizations/PortalAgents", UriKind.Relative))
        {
        }

        public void EnterPassword(string inputPassword)
        {
            password.SendKeys(inputPassword);
        }

        public void EnterConfirmationPassword(string inputConfirmationPassword)
        {
            confirmationPassword.SendKeys(inputConfirmationPassword);
        }
    }
}
