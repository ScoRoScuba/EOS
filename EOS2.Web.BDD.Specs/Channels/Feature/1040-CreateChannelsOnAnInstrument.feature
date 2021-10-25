@StoryId_1040
@CreateChannelsOnAnInstrument
Feature: 1040-CreateChannelsOnAnInstrument
	As a Customer Site Administrator
	I want to create n channel(s) against an instrument
	so that I can associate channels to equipment

Scenario: CreateDefaultChannels
	Given I am logged in as the EOS Owner 'kim.roberts' with the password '!12345678A'
	And I have navigated to details page for '1040Plant Area'
	And I see an instrument with the name '1040Instrument' is shown
	When I click on the 'Channels' button
	Then Then i am shown the Channels page for '1040Instrument' 
	And the section 'Add Channels' is shown
	When I set the 'number of channels' to be created to '5' the 'type of channel' to 'analogue' and the 'frequency' to 'None'
	And I click on the 'Save' button
	Then I am shown a channel list with '5' rows


