namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;

    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.SetUp;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class AddOrganizationPage : BasePageObject
    {
        [FindsBy(How = How.Id, Using = "Name")]
        private readonly IWebElement name = null;

        [FindsBy(How = How.Id, Using = "Address")]
        private readonly IWebElement address = null;

        [FindsBy(How = How.Id, Using = "PostalCode")]
        private readonly IWebElement postalCode = null;

        [FindsBy(How = How.CssSelector, Using = "form > div.form-group.input-options > button")]
        private readonly IWebElement saveButton = null;

        [FindsBy(How = How.CssSelector, Using = "form > div.form-group.input-options > a")]
        private readonly IWebElement cancelButton = null;

        public AddOrganizationPage(IWebDriver driver, OrganizationType organization)
            : base(driver, new Uri(string.Format(CultureInfo.InvariantCulture, "/Organizations/{0}/New", organization), UriKind.Relative))
        {
        }

        public string GetName
        {
            get
            {
                return name.GetAttribute("value").ToString();
            }
        }

        public string GetAddress
        {
            get
            {
                return address.Text;
            }
        }

        public string GetPostalCode
        {
            get
            {
                return postalCode.GetAttribute("value").ToString();
            }
        }

        internal ReadOnlyCollection<IWebElement> GetPortalAgentsDetails
        {
            get
            {
                return BeforeAfterTests.Driver.FindElements(By.CssSelector("div.table-responsive table tbody tr"));
            }
        }

        public void FillInForm(string organizationName, string organizationAddress, string organizationPostalCode)
        {
            SetOrganizationName(organizationName);
            SetOrganizationAddress(organizationAddress);
            SetOrganizationPostalCode(organizationPostalCode);
        }

        public void SetOrganizationName(string organizationName)
        {
            name.SendKeys(organizationName);
        }

        public void SetOrganizationAddress(string organizationAddress)
        {
            address.Clear();
            address.SendKeys(organizationAddress);
        }

        public void SetOrganizationPostalCode(string organizationPostalCode)
        {
            postalCode.SendKeys(organizationPostalCode);
        }
            
        public void ClickSave()
        {
            saveButton.Click();
        }

        public void ClickCancel()
        {
            cancelButton.Click();
        }

        public void ClickLink(string text)
        {
            BeforeAfterTests.Driver.FindElement(By.LinkText(text)).Click();
        }

        public void ClickDetailsButton(int index)
        {
            var agentDetails = GetPortalAgentsDetails;
            var agentDetailsButton = agentDetails[index].FindElement(By.CssSelector("div.table-responsive table tbody tr td a"));
            agentDetailsButton.Click();
        }

        public void ClickManageUserButton(int index)
        {
            var agentDetails = GetPortalAgentsDetails;
            var agentManageuserButton = agentDetails[index].FindElements(By.CssSelector("div.table-responsive table tbody tr td a"));
            agentManageuserButton[1].Click();
        }

        public bool ClickSetPasswordButtonExists(int index)
        {
            var agentDetails = GetPortalAgentsDetails;
            var agentSetPasswordButton = agentDetails[index].FindElements(By.CssSelector("div.table-responsive table tbody tr td a"));
            return agentSetPasswordButton[1].Displayed;
        }

        public bool ClickLinkIsExists(string text)
        {
         return BeforeAfterTests.Driver.FindElement(By.LinkText(text)).Displayed;
        }
    }
}
