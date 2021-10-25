@StoryId_1041
@CreateChannelsOnAnInstrument
Feature: 1041-EditChannels
	As a Customer Site Administrator
	I want to edit channels
	So that I can maintain my channels.

Scenario: Edit Channel Name
	Given I am logged in as the EOS Owner 'kim.roberts' with the password '!12345678A'
	And I have navigated to details page for '1041Plant Area'
	And I see an instrument with the name '1041Instrument' is shown
	When I click on the 'Channels' button
	Then Then I am shown the Channels page for '1041Instrument' 
	When I select the 'Name' field for Channel '0000'
	And change it to 'My Name has Changed'
	When I click on the 'Save' button for that row
	Then The channel is saved with the correct name