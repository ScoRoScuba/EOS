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
namespace EOS2.Web.BDD.Specs.ServiceProvider.Feature
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("423-EditInstrument")]
    [NUnit.Framework.CategoryAttribute("StoryId_423")]
    public partial class _423_EditInstrumentFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "423-EditInstrument.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "423-EditInstrument", "      As a Site Administrator or a Service Provider User\r\n      I want to be able" +
                    " to edit an Instrument \r\n      So that I can change meta-data", ProgrammingLanguage.CSharp, new string[] {
                        "StoryId_423"});
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
        [NUnit.Framework.DescriptionAttribute("Cancel from Instrument Details page")]
        [NUnit.Framework.CategoryAttribute("SingleInstrument")]
        public virtual void CancelFromInstrumentDetailsPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cancel from Instrument Details page", new string[] {
                        "SingleInstrument"});
#line 9
this.ScenarioSetup(scenarioInfo);
#line 10
 testRunner.Given("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 11
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.When("I press the \'Cancel\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.Then("I am on the \'Plant Area Details\' page for plant area \'423-Test Plant Area 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Save empty form from Instrument Details page")]
        [NUnit.Framework.CategoryAttribute("SingleInstrument")]
        public virtual void SaveEmptyFormFromInstrumentDetailsPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save empty form from Instrument Details page", new string[] {
                        "SingleInstrument"});
#line 16
this.ScenarioSetup(scenarioInfo);
#line 17
 testRunner.Given("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 18
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
    testRunner.And("I have cleared the \'Name\' textbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
    testRunner.And("I have selected \'Please Select ...\' from the \'Instrument Type\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
    testRunner.And("I have selected \'Please Select ...\' from the \'Calibration Frequency\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.When("I press the \'Save\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
    testRunner.Then("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
    testRunner.And("I am shown the message \"Please enter the name for your instrument\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
    testRunner.And("I am shown the message \"Please select a Type\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
    testRunner.And("I am shown the message \'Please select a Calibration Frequency\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Save form from Instrument Details page with invalid name")]
        [NUnit.Framework.CategoryAttribute("SingleInstrument")]
        public virtual void SaveFormFromInstrumentDetailsPageWithInvalidName()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save form from Instrument Details page with invalid name", new string[] {
                        "SingleInstrument"});
#line 29
this.ScenarioSetup(scenarioInfo);
#line 30
 testRunner.Given("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
    testRunner.And("I have entered \'12345678901234567890123456789012345678901234567890123456789012345" +
                    "67890123456789012345678901234567890123456789012345678901\' in the \'Name\' textbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
    testRunner.And("I have selected \'Analyzer\' in the \'Instrument Type\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
    testRunner.And("I have selected \'None\' in the \'Calibration Frequency\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.When("I press the \'Save\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
    testRunner.And("I am shown the message \"Maximum Length is 120 Characters\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Save form from Instrument Details page with existing name")]
        [NUnit.Framework.CategoryAttribute("MultipleInstrument")]
        public virtual void SaveFormFromInstrumentDetailsPageWithExistingName()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save form from Instrument Details page with existing name", new string[] {
                        "MultipleInstrument"});
#line 40
this.ScenarioSetup(scenarioInfo);
#line 41
 testRunner.Given("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 42
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
    testRunner.And("I have entered \'423-Test Instrument 2\' in the \'Name\' textbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
    testRunner.And("I have selected \'Analyzer\' in the \'Instrument Type\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
    testRunner.And("I have selected \'None\' in the \'Calibration Frequency\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.When("I press the \'Save\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
    testRunner.Then("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 48
    testRunner.And("I am shown the message \"An instrument with that Name already exists for this Plan" +
                    "t Area\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Save complete form from Instrument Details page")]
        [NUnit.Framework.CategoryAttribute("SingleInstrument")]
        public virtual void SaveCompleteFormFromInstrumentDetailsPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save complete form from Instrument Details page", new string[] {
                        "SingleInstrument"});
#line 51
this.ScenarioSetup(scenarioInfo);
#line 52
 testRunner.Given("I am signed in as \'sam.morris\' with the password \'!12345678A\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
    testRunner.And("I have entered \'423-Test Instrument\' in the \'Name\' textbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
    testRunner.And("I have selected \'Analyzer\' in the \'Instrument Type\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
    testRunner.And("I have selected \'None\' in the \'Calibration Frequency\' dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.When("I press the \'Save\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
    testRunner.And("I am on the \'Instrument Details\' page for customer \'423-Test Customer 1\', site \'4" +
                    "23-Test Site 1\', plant area \'423-Test Plant Area 1\' and instrument \'423-Test Ins" +
                    "trument\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
    testRunner.And("I am shown the message \"Instrument 423-Test Instrument saved successfully\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion