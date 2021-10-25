namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class SignInPage : BasePageObject
    {
        [FindsBy(How = How.Name, Using = "UserName")]
        private readonly IWebElement userName = null;

        [FindsBy(How = How.Name, Using = "Password")]
        private readonly IWebElement password = null;

        [FindsBy(How = How.CssSelector, Using = "form input[value='Sign In']")]
        private readonly IWebElement signIn = null;

        [FindsBy(How = How.CssSelector, Using = "a[href*='SignOut']")]
        private readonly IWebElement signOut = null;

        [FindsBy(How = How.CssSelector, Using = "a[href*='SignIn']")]
        private readonly IWebElement signin = null;

        public SignInPage(IWebDriver driver)
            : base(driver, new Uri("/Account/SignIn", UriKind.Relative))
        {
        }

        public bool IsSignInLinkExists
        {
            get
            {
                return signin.Displayed;
            }
        }

        public void SignIn(string p0, string p1)
        {
            userName.SendKeys(p0);
            password.SendKeys(p1);
            signIn.Click();
        }

        public void SignIn()
        {
            signin.Click();
        }

        public void SignOut()
        {
            signOut.Click();
        }
    }
}
