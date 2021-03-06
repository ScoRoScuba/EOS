// ------------------------------------------------------------------------------
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
    [NUnit.Framework.DescriptionAttribute("340-ViewPlantArea")]
    [NUnit.Framework.CategoryAttribute("StoryId_340")]
    public partial class _340_ViewPlantAreaFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "340-ViewPlantArea.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "340-ViewPlantArea", "  As a Service Provider User\r\n  I want to see a list of plant areas within a sele" +
                    "cted site\r\n  So that I can select one", ProgrammingLanguage.CSharp, new string[] {
                        "StoryId_340"});
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
        
        public virtual void FeatureBackground()
        {
#line 7
#line 8
    testRunner.Given("I am signed in as a \'ServiceProvider\' \'user\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
     testRunner.And("A Plant Area exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View Plant Area List")]
        public virtual void ViewPlantAreaList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View Plant Area List", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 12
    testRunner.When("I go to the Site Details page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
    testRunner.Then("A Plant Area is listed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Select a Plant Area")]
        public virtual void SelectAPlantArea()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Select a Plant Area", ((string[])(null)));
#line 15
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 16
     testRunner.And("I go to the Site Details page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
     testRunner.And("A Plant Area is listed with a \'Details\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
    testRunner.When("I click on the \'Details\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
    testRunner.Then("I am on the Plant Area Details page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View Plant Area Details")]
        public virtual void ViewPlantAreaDetails()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View Plant Area Details", ((string[])(null)));
#line 21
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 22
     testRunner.And("I go to the Plant Area Details page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
    testRunner.Then("I am shown the correct data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
