namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    public class OrganizationsCustomerPage : BasePageObject
    {
        [FindsBy(How = How.CssSelector, Using = "#SelectedLanguageId")]
        private readonly IWebElement languageSelector = null;

        [FindsBy(How = How.CssSelector, Using = "#langform > button")]
        private readonly IWebElement languageButton = null;

        [FindsBy(How = How.CssSelector, Using = "#content > h2")]
        private readonly IWebElement pageHeading = null;

        [FindsBy(How = How.CssSelector, Using = "#add-organization")]
        private readonly IWebElement addButton = null;

        [FindsBy(How = How.CssSelector, Using = "#organizations > thead > tr > th:nth-child(4)")]
        private readonly IWebElement organizationsTableHeading = null;

        public OrganizationsCustomerPage(IWebDriver driver)
            : base(driver, new Uri("/Organizations/Customer", UriKind.Relative))
        {
        }

        public string PageHeading
        {
            get
            {
                return pageHeading.Text;
            }
        }

        public string AddButtonText
        {
            get
            {
                return addButton.Text;
            }
        }

        public string OrganizationsTableHeadingText
        {
            get
            {
                return organizationsTableHeading.Text;
            }
        }

        public void ChooseLanguage(string language)
        {
            var select = new SelectElement(languageSelector);
            select.SelectByText(language);
            languageButton.Click();
        }

        public void ClickAddOrganization()
        {
            addButton.Click();
        }
    }
}
