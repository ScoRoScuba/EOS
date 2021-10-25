@StoryId_423
Feature: 423-EditInstrument
        As a Site Administrator or a Service Provider User
        I want to be able to edit an Instrument 
        So that I can change meta-data


@SingleInstrument
Scenario: Cancel from Instrument Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
	When I press the 'Cancel' button
	Then I am on the 'Plant Area Details' page for plant area '423-Test Plant Area 1'

@SingleInstrument
Scenario: Save empty form from Instrument Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I have cleared the 'Name' textbox
    And I have selected 'Please Select ...' from the 'Instrument Type' dropdown
    And I have selected 'Please Select ...' from the 'Calibration Frequency' dropdown
	When I press the 'Save' button
    Then I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I am shown the message "Please enter the name for your instrument"
    And I am shown the message "Please select a Type"
    And I am shown the message 'Please select a Calibration Frequency'

@SingleInstrument
Scenario: Save form from Instrument Details page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
    And I have selected 'Analyzer' in the 'Instrument Type' dropdown
    And I have selected 'None' in the 'Calibration Frequency' dropdown
	When I press the 'Save' button
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I am shown the message "Maximum Length is 120 Characters"

@MultipleInstrument
Scenario: Save form from Instrument Details page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I have entered '423-Test Instrument 2' in the 'Name' textbox
    And I have selected 'Analyzer' in the 'Instrument Type' dropdown
    And I have selected 'None' in the 'Calibration Frequency' dropdown
	When I press the 'Save' button
    Then I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I am shown the message "An instrument with that Name already exists for this Plant Area"

@SingleInstrument
Scenario: Save complete form from Instrument Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument 1'
    And I have entered '423-Test Instrument' in the 'Name' textbox
    And I have selected 'Analyzer' in the 'Instrument Type' dropdown
    And I have selected 'None' in the 'Calibration Frequency' dropdown
	When I press the 'Save' button
    And I am on the 'Instrument Details' page for customer '423-Test Customer 1', site '423-Test Site 1', plant area '423-Test Plant Area 1' and instrument '423-Test Instrument'
    And I am shown the message "Instrument 423-Test Instrument saved successfully"