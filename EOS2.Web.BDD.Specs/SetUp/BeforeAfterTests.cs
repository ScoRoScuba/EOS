namespace EOS2.Web.BDD.Specs.SetUp
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;
    using EOS2.Web.BDD.Specs.App_Start;

    using Microsoft.AspNet.Identity;
    using Microsoft.Practices.Unity;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Safari;

    using TechTalk.SpecFlow;

    [Binding]
    public class BeforeAfterTests
    {
        private static int defaultImplicitWaitTime;

        public static UnityContainer DependencyContainer { get; private set; }

        public static IWebDriver Driver { get; set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Set up Unity so that the given parts of the tests can use the services layer to set up test data.
            DependencyContainer = new UnityContainer();
            UnityConfig.RegisterTypes(DependencyContainer);            
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            DependencyContainer.Dispose();            
        }

        [BeforeFeature]
        public static void Setup()
        {
            switch (ConfigurationManager.AppSettings["seleniumDriver"])
            {
                case "Firefox":
                    Driver = new FirefoxDriver();
                    break;
                case "Internet Explorer":
                    Driver = new InternetExplorerDriver();
                    break;
                case "Chrome":
                    Driver = new ChromeDriver();
                    break;
                case "Safari":
                    Driver = new SafariDriver();
                    break;
                default:
                    throw new ConfigurationErrorsException(
                        "Please set seleniumDriver in App.Config to be either 'Firefox', 'Internet Explorer', 'Chrome' or 'Safari'");
            }

            Driver.Manage().Cookies.DeleteAllCookies();
            defaultImplicitWaitTime = Convert.ToInt16(ConfigurationManager.AppSettings.Get("DefaultImplicitWaitTime"), CultureInfo.InvariantCulture);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(defaultImplicitWaitTime));
        }

        [AfterFeature]
        public static void Teardown()
        {
            Driver.Quit();
            Driver = null;
        }

        [BeforeScenario]
        public static void ScenarioSetup()
        {
            DatabaseMaintenance.Reset();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is a bets served as a function")]
        public static string GetStoryId()
        {
            var featureTitle = FeatureContext.Current.FeatureInfo.Tags.SingleOrDefault(t => t.StartsWith("StoryId_", StringComparison.Ordinal));
            return !string.IsNullOrWhiteSpace(featureTitle) ? Regex.Match(featureTitle, @"[^_]*$").ToString() : "999999";
        }
    }
}
