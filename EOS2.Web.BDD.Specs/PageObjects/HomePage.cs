namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;
 
    using EOS2.Web.BDD.Specs.SetUp;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    public class HomePage : BasePageObject
    {
        private const string Password = "!12345678A";

        [FindsBy(How = How.CssSelector, Using = "#SelectedLanguageId")]
        private readonly IWebElement languageSelector = null;

        [FindsBy(How = How.CssSelector, Using = "form>div.language>button[type='submit']")]
        private readonly IWebElement languageButton = null;

        [FindsBy(How = How.CssSelector, Using = "div.navbar > div > div.navbar-header > a")]
        private readonly IWebElement pageTitle = null;

        [FindsBy(How = How.CssSelector, Using = "h2")]
        private readonly IWebElement headerTitle = null;

        public HomePage(IWebDriver driver)
            : base(driver, new Uri("/", UriKind.Relative))
        {
        }

        public string PageTitle
        {
            get
            {
                return pageTitle.Text;
            }
        }

        public string GetHeaderTitle
        {
            get
            {
                return headerTitle.Text;
            }
        }

        public void ChooseLanguage(string language)
        {
            var select = new SelectElement(languageSelector);
            select.SelectByText(language);
            languageButton.Click();
        }

        public void SignIn(string userName, string password)
        {
            var signInPage = new SignInPage(BeforeAfterTests.Driver);
            signInPage.SignIn(userName, password);
        }

        public void SignIn(string userName)
        {
            SignIn(userName, Password);
        }
    }
}
