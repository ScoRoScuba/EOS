@StoryId_1042
@CreateInstrumentChannelsAndEquipment
Feature: 1042-ManageChannelAssociationForEquipemt
As a Customer Site Administrator 
I want to attach / detach channel groups from equipment
So that I can accurately represent my plant.

#Note: I want to do this from either the Channel Groups page or the Equipment detail page.

Scenario: Associate Equipment When Viewing Channels
	Given I am logged in as the EOS Owner 'kim.roberts' with the password '!12345678A'
	And I have navigated to details page for '1042Plant Area'
	And I am looking at the channels for '1042Instrument' 
	When I Select the Attached to Equipment for Channel '0000' 
	And and Select '1042Equipment'
	And press Save
	Then The channel is associated to '1042Equipment'


Scenario: Associate Channels from Equipment When Viewing
	Given I am logged in as the EOS Owner 'kim.roberts' with the password '!12345678A'
	And I have navigated to details page for '1042Plant Area'
	And I am looking at the Details for '1042Equipment'
	When I select 'Attach To Channels"
	Then I select '1042Instrument' 
	And click on 'Select Channels'
	When I Select the Attached to Equipment for Channel '0001' 
	Then I am only able to select '1042Equipment' or 'None'
	When I Select '1042Equipment'
	And press Save
	Then The channel is associated to '1042Equipment'
	And Channel '0001' Is removed from the list
