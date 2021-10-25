@StoryId_891
Feature: 891-CreateSchedule
    As a Site Administrator or a Service Provider User
    I want to be able to create a TUS or SAT schedule for my equipment
    So that I can facilitate planning of TUS or SAT surveys

@SingleEquipment
Scenario: Get to New Schedule page
    Given I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the 'Equipment Details' page
    And the add button text is 'Add Schedule'
    When I click the 'Add Schedule' button
    Then the 'New Schedule' page is displayed

@SingleEquipment
Scenario: Cancel from New Schedule page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Schedule' page
	When I press the 'Cancel' button
	Then I am on the 'Equipment Details' page

@SingleEquipment
Scenario: Save empty form from New Schedule page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Schedule' page
	When I press the 'Save' button
	Then I am on the 'New Schedule' page
    And I am shown the message "Please enter the name for your schedule"
    And I am shown the message "Please select a Furnace Class"
    And I am shown the message 'Please select a Type'
    And I am shown the message 'Please select a Frequency'

@SingleEquipment
Scenario: Save form from New Schedule page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Schedule' page
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
    And I have selected '-' in the 'Furnace Class' dropdown
    And I have selected 'None' in the 'Frequency' dropdown
    And I have selected 'SAT' in the 'Schedule Type' radio button
	When I press the 'Save' button
	Then I am on the 'New Schedule' page
    And I am shown the message "Maximum Length is 120 Characters"

@SingleSchedule
Scenario: Save form from New Schedule page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Schedule' page
    And I have entered '891-Test Schedule 1' in the 'Name' textbox
    And I have selected '1' in the 'Furnace Class' dropdown
    And I have selected 'Weekly' in the 'Frequency' dropdown
    And I have selected 'TUS' in the 'Schedule Type' radio button
	When I press the 'Save' button
	Then I am on the 'New Schedule' page
    And I am shown the message "A Schedule with that Name already exists for this Equipment"

@SingleSchedule
Scenario: Save form from New Schedule page with existing Schedule for Class and Type
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Schedule' page
    And I have entered '891-Test Schedule' in the 'Name' textbox
    And I have selected '-' in the 'Furnace Class' dropdown
    And I have selected 'None' in the 'Frequency' dropdown
    And I have selected 'SAT' in the 'Schedule Type' radio button
	When I press the 'Save' button
	Then I am on the 'New Schedule' page
    And I am shown the message "A Schedule of that Type & Furnace Class already exists for this Equipment"

@SingleSchedule
Scenario: Save complete form from New Schedule page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Schedule' page
    And I have entered '891-Test Schedule' in the 'Name' textbox
    And I have selected '-' in the 'Furnace Class' dropdown
    And I have selected 'None' in the 'Frequency' dropdown
    And I have selected 'SAT' in the 'Schedule Type' radio button
	When I press the 'Save' button
	Then I am on the ' Details' page
    And I am shown the message "Schedule 891-Test Schedule saved successfully"

