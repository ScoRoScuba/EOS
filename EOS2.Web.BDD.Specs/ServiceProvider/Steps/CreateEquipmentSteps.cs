namespace EOS2.Web.BDD.Specs.ServiceProvider.Steps
{
    using System.Configuration;
    using EOS2.Model.Enums;
    using EOS2.Web.BDD.Specs.PageObjects;
    using EOS2.Web.BDD.Specs.SetUp;
    using NUnit.Framework;
    using OpenQA.Selenium.IE;
    using TechTalk.SpecFlow;

 [Binding]
 public class CreateEquipmentSteps
 {
 [When(@"I select the '(.*)' button for Customer '(.*)'")]
 public void WhenISelectTheButtonForCustomer(string p0, string p1)
 {
 }

[When(@"I select the '(.*)' button for Site '(.*)'")]
public void WhenISelectTheButtonForSite(string p0, string p1)
{
}

[When(@"I select the '(.*)' button for '(.*)'")]
public void WhenISelectTheButtonFor(string p0, string p1)
{
}

[When(@"I select the '(.*)' button")]
public void WhenISelectTheButton(string p0)
{
}

[When(@"I set '(.*)' to '(.*)'")]
public void WhenISetTo(string p0, string p1)
{
}

[When(@"I set '(.*)'A Furnace Model'")]
public void WhenISetAFurnaceModel(string p0)
{
}

[Then(@"I am shown Details page for  '(.*)'")]
public void ThenIAmShownDetailsPageFor(string p0)
{
}

[Then(@"I am shown Details for '(.*)'")]
 public void ThenIAmShownDetailsFor(string p0)
{
}

[Then(@"I am shown the Details for '(.*)'")]
public void ThenIAmShownTheDetailsFor(string p0)
{
}

[Then(@"The Title has changed to '(.*)'")]
public void ThenTheTitleHasChangedTo(string p0)
{
}

[Then(@"I am able to Add '(.*)'")]
public void ThenIAmAbleToAdd(string p0)
{
}

[Then(@"I am shown the message '(.*)' under the '(.*)' field")]
 public void ThenIAmShownTheMessageUnderTheField(string p0, string p1)
{
}

[Then(@"I am shown the Details page for '(.*)'")]
public void ThenIAmShownTheDetailsPageFor(string p0)
{
}

[Then(@"The title says the '(.*)'")]
public void ThenTheTitleSaysThe(string p0)
{
}

[When(@"I edit the '(.*)' to '(.*)'")]
public void WhenIEditTheTo(string p0, string p1)
{
}
    }
}
