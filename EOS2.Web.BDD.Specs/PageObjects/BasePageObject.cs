namespace EOS2.Web.BDD.Specs.PageObjects
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Security.Policy;

    using EOS2.Web.BDD.Specs.SetUp;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.PageObjects;

    using TechTalk.SpecFlow;

    public abstract class BasePageObject
    {
        private readonly Uri absoluteUri = null;

        protected BasePageObject(IWebDriver driver, Uri relativeUrl)
        {
            if (driver == null) throw new ArgumentNullException("driver");
            if (relativeUrl == null) throw new ArgumentNullException("relativeUrl");
            if (!relativeUrl.IsWellFormedOriginalString()) throw new ArgumentException("Badly formed URL", "relativeUrl");
            
            // Get the base url of the test system from the application settings and add on the relative url which is passed in.
            absoluteUri = new Uri(new Uri(ConfigurationManager.AppSettings["seleniumBaseUrl"]), relativeUrl);
            driver.Navigate().GoToUrl(absoluteUri);
            PageFactory.InitElements(driver, this);
        }

        public void SetBrowserLanguagePreference(string language)
        {
            if (string.IsNullOrEmpty(language)) throw new ArgumentNullException("language");

            var languages = AcceptLanguages(language);

            if (BeforeAfterTests.Driver.GetType() == typeof(FirefoxDriver))
            {
                BeforeAfterTests.Driver.Quit();
                var profile = new FirefoxProfile();
                profile.SetPreference("intl.accept_languages", languages);
                BeforeAfterTests.Driver = new FirefoxDriver(profile);
            }
            else if (BeforeAfterTests.Driver.GetType() == typeof(ChromeDriver))
            {
                BeforeAfterTests.Driver.Quit();
                var options = new ChromeOptions();
                options.AddUserProfilePreference("intl.accept_languages", languages);
                BeforeAfterTests.Driver = new ChromeDriver(options);
            }
            else if (BeforeAfterTests.Driver.GetType() == typeof(InternetExplorerDriver))
            {
                throw new SpecFlowException("The accepted languages cannot be set in Internet Explorer");
            }

            BeforeAfterTests.Driver.Navigate().GoToUrl(absoluteUri);
            PageFactory.InitElements(BeforeAfterTests.Driver, this);
        }

        private static string AcceptLanguages(string language)
        {
            string languages = string.Empty;
            switch (language)
            {
                case "Afrikaans":
                    languages = "af,en-gb";
                    break;
                case "English (United Kingdom)":
                    languages = "en-gb,af";
                    break;
                case "English (United States)":
                    languages = "en-us,af";
                    break;
            }

            return languages;
        }
    }
}
