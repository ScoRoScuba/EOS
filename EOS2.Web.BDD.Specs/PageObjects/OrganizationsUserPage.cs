namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    public class OrganizationsUserPage : BasePageObject
    {
        [FindsBy(How = How.CssSelector, Using = "a[href*='Add']")]
       private readonly IWebElement addUserButton = null;

       [FindsBy(How = How.Id, Using = "UserName")]
       private readonly IWebElement userName = null;

       [FindsBy(How = How.Id, Using = "Name")]
       private readonly IWebElement name = null;

       [FindsBy(How = How.Id, Using = "FamilyName")]
       private readonly IWebElement familyName = null;

       [FindsBy(How = How.Id, Using = "Email")]
       private readonly IWebElement emailAddress = null;

       [FindsBy(How = How.Id, Using = "ConfirmationEmail")]
       private readonly IWebElement confirmationEmailAddress = null;

       [FindsBy(How = How.CssSelector, Using = "div.alert.alert-success.alert-dismissable")]
       private readonly IWebElement successMessage = null;

       [FindsBy(How = How.CssSelector, Using = "span.field-validation-error")]
       private readonly IWebElement validationMessage = null;

        public OrganizationsUserPage(IWebDriver driver)
            : base(driver, new Uri("/Organizations/Users", UriKind.Relative))
        {
        }

       public string SuccessMessageTitle
        {
            get { return successMessage.Text; }
        }

       public string ValidationMessageTitle
       {
           get { return validationMessage.Text; }
       }

        public void ClickAddUser()
        {
            addUserButton.Click();
        }

       public void EnterUserName(string inputUserName)
        {
            userName.SendKeys(inputUserName);
        }

       public void EnterName(string inputName)
       {
           name.SendKeys(inputName);
       }

       public void EnterFamilyName(string inputFamilyName)
       {
           familyName.SendKeys(inputFamilyName);
       }

       public void EnterEmailAddress(string inputEmail)
       {
           emailAddress.SendKeys(inputEmail);
       }

       public void EnterConfirmationEmailAddress(string inputConfirmationEmailAddress)
       {
           confirmationEmailAddress.SendKeys(inputConfirmationEmailAddress);
       }
    }
}
