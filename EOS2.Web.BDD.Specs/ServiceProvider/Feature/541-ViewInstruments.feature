@StoryId_541
Feature: 541-ViewInstruments
    As a Site Administrator or a Service Provider User
    I want to be able to view a list of instruments attached to equipment
    So that I can see a list of instruments on that piece of equipment

@SingleInstrument
Scenario: Select Instrument to View
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Plant Area Details' page for customer '541-Test Customer 1', site '541-Test Site 1' and plant area '541-Test Plant Area 1'
    And Instrument '541-Test Instrument 1' is listed
    And Instrument '541-Test Instrument 1' has a 'Details' button
	When I press the 'Details' button
	Then I am on the 'Instrument Details' page
    And the 'Name' textbox displays '541-Test Instrument 1'
    And the 'Description' textbox displays 'Test Description 1'
    And the 'Make' textbox displays 'Test Make 1'
    And the 'Model' textbox displays 'Test Model 1'
    And the 'Instrument Type' dropdown displays 'Analyser'
    And the 'Calibration Frequency' dropdown displays 'None'
    And the 'Serial Number' textbox displays 'I1'
    And the 'Number of Channels' textbox displays '1'
    And the 'Notes' textbox displays 'Test Notes 1'