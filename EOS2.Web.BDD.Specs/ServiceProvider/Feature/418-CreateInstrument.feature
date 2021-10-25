@StoryId_418
Feature: 418-CreateInstrument
        As a Service Provider User
        I want to be able to create an Instrument which is unassigned
        So that I can record calibration events and results against that equipment

        As a Service Provider User
        I want to be able to create an Instrument and assign it to multiple pieces of equipment
        So that I can record calibration events and results against that equipment


@SinglePlantArea
Scenario: Get to New Instrument page
    Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Plant Area Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And the add button text is 'Add Instrument'
    When I click the 'Add Instrument' button
    Then the 'New Instrument' page is displayed

@SinglePlantArea
Scenario: Cancel from New Instrument page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'New Instrument' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
	When I press the 'Cancel' button
	Then I am on the 'Plant Area Details' page for plant area '418-Test Plant Area 1'

@SinglePlantArea
Scenario: Save empty form from New Instrument page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'New Instrument' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
	When I press the 'Save' button
	Then I am on the 'New Instrument' page
    And I am shown the message "Please enter the name for your instrument"
    And I am shown the message "Please select a Type"
    And I am shown the message 'Please select a Calibration Frequency'

@SinglePlantArea
Scenario: Save form from New Instrument page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'New Instrument' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
    And I have selected 'Analyzer' in the 'Instrument Type' dropdown
    And I have selected 'None' in the 'Calibration Frequency' dropdown
	When I press the 'Save' button
	Then I am on the 'New Instrument' page
    And I am shown the message "Maximum Length is 120 Characters"

@SingleInstrument
Scenario: Save form from New Instrument page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'New Instrument' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I have entered '418-Test Instrument 1' in the 'Name' textbox
    And I have selected 'Analyzer' in the 'Instrument Type' dropdown
    And I have selected 'None' in the 'Calibration Frequency' dropdown
	When I press the 'Save' button
	Then I am on the 'New Instrument' page
    And I am shown the message "An instrument with that Name already exists for this Plant Area"

@SingleInstrument
Scenario: Save complete form from New Instrument page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'New Instrument' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I have entered '418-Test Site' in the 'Name' textbox
    And I have selected 'Analyzer' in the 'Instrument Type' dropdown
    And I have selected 'None' in the 'Calibration Frequency' dropdown
	When I press the 'Save' button
	Then I am on the 'Instrument Details' page
    And I am shown the message "Instrument 418-Test Site saved successfully"

@SingleInstrumentSingleEquipment
Scenario: Attach an Instrument to a piece of Equipment (single Equipment Exists)
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And The 'Equipment Attached' panel is displayed
    And I have selected '418-Test Equipment 1' from the 'Equipment Attached' dropdown
    When I press the 'Attach Selected Equipment' button
    Then I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I am shown the message 'Equipment attached successfully'
    And '418-Test Equipment 1' is listed in the 'Equipment Attached' panel
    And The 'Equipment Attached' dropdown is not displayed
    And The 'Attach Selected Equipment' button is not displayed
    
@SingleInstrumentMultipleEquipment
Scenario: Attach an Instrument to a piece of Equipment (multiple Equipment Exists)
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And The 'Equipment Attached' panel is displayed
    And I have selected '418-Test Equipment 1' from the 'Equipment Attached' dropdown
    When I press the 'Attach Selected Equipment' button
    Then I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I am shown the message 'Equipment attached successfully'
    And '418-Test Equipment 1' is listed in the 'Equipment Attached' panel
    And The 'Equipment Attached' dropdown is displayed
    And '418-Test Equipment 1' is not in the dropdown
    And The 'Attach Selected Equipment' button is displayed

@SingleInstrumentMultipleEquipmentSingleAttached
Scenario: Attach an Instrument to a multiple pieces of Equipment 
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And The 'Equipment Attached' panel is displayed
    And '418-Test Equipment 1' is listed in the 'Equipment Attaached' panel
    And I have selected '418-Test Equipment 2' from the 'Equipment Attached' dropdown
    When I press the 'Attach Selected Equipment' button
    Then I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I am shown the message 'Equipment attached successfully'
    And '418-Test Equipment 1' is listed in the 'Equipment Attached' panel
    And '418-Test Equipment 2' is listed in the 'Equipment Attached' panel

@SingleInstrumentMultiEquipmentSingleAttached
Scenario: Detach an Instrument from a piece of Equipment
	Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And The 'Equipment Attached' panel is displayed
    And '418-Test Equipment 1' is listed in the 'Equipment Attaached' panel
    And '418-Test Equipment 1' has a 'Detach' button
    When I press the 'Detach' button
    Then I am on the 'Instrument Details' page for customer '418-Test Customer 1', site '418-Test Site 1' and plant area '418-Test Plant Area 1'
    And I am shown the message 'Equipment 418-Test Equipment 1 detached successfully'
    And '418-Test Equipment 1' is not listed in the 'Equipment Attached' panel
    And '418-Test Equipment 1' is listed in the 'Equipment Attached' dropdown
 