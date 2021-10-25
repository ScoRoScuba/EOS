﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18444
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace EOS2.Web.BDD.Specs.Internationalization.Feature
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("698-Internationalization")]
    [NUnit.Framework.CategoryAttribute("Internationalization")]
    public partial class _698_InternationalizationFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "698-Internationalization.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "698-Internationalization", "  As a User\r\n  I want to see EOS in my own language\r\n  So that I can understand a" +
                    "nd use EOS", ProgrammingLanguage.CSharp, new string[] {
                        "Internationalization"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Correctly choose localization based on browser settings")]
        public virtual void CorrectlyChooseLocalizationBasedOnBrowserSettings()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Correctly choose localization based on browser settings", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("my browser is set to prefer \'Afrikaans\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("EOS language selection is \'|Auto|\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the Eurotherm Online Services title should be surrounded by vertical bars", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Set the locale manually")]
        public virtual void SetTheLocaleManually()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Set the locale manually", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
    testRunner.Given("my browser is set to prefer \'English (United Kingdom)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
    testRunner.And("EOS language selection is \'Auto\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
    testRunner.And("the Eurotherm Online Service title is not surrounded by vertical bars", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
    testRunner.When("I choose \'Afrikaans\' from the language selector and click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
    testRunner.Then("the Eurotherm Online Services title should be surrounded by vertical bars", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Switch between UK and US English")]
        public virtual void SwitchBetweenUKAndUSEnglish()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Switch between UK and US English", ((string[])(null)));
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
    testRunner.Given("my browser is set to prefer \'English (United Kingdom)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
    testRunner.And("EOS language selection is \'Auto\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
    testRunner.And("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
    testRunner.And("I am on the Customer Organizations page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
    testRunner.And("the organization table heading is \'Organisation Types\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
    testRunner.When("I choose \'English (United States)\' from the language selector on the Customer Org" +
                    "anizations page and click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
    testRunner.Then("the organization table heading is \'Organization Types\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 27
    testRunner.When("I choose \'English (United Kingdom)\' from the language selector on the Customer Or" +
                    "ganizations page and click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
    testRunner.Then("the organization table heading is \'Organisation Types\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("POST request retains language selection")]
        public virtual void POSTRequestRetainsLanguageSelection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("POST request retains language selection", ((string[])(null)));
#line 30
this.ScenarioSetup(scenarioInfo);
#line 31
    testRunner.Given("my browser is set to prefer \'English (United Kingdom)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 32
    testRunner.And("EOS language selection is \'Auto\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
    testRunner.And("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
    testRunner.And("I am on the Customer Organizations page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
    testRunner.And("the organization table heading is \'Organisation Types\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
    testRunner.When("I choose \'English (United States)\' from the language selector on the Customer Org" +
                    "anizations page and click Ok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 37
    testRunner.And("I click the Add Customer Organization button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
    testRunner.And("I set the name to \'Widgets R Us\' and the address to \'1 The Street\' and the Postal" +
                    " Code to \'XY13 KH12\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
    testRunner.And("I click the Save Button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
    testRunner.And("I click the\'Customers\'link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
    testRunner.Then("the organization table heading is \'Organization Types\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
