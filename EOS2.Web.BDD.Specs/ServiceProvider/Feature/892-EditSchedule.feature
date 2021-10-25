@StoryId_892
Feature: 892-EditSchedule
    As a Site Administrator or a Service Provider User
    I want to be able to edit a TUS or SAT schedule for my equipment
    So that I can facilitate planning of TUS or SAT surveys


@SingleSchedule
Scenario: Get to Schedule Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Equipment Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1' and equipment '892-Test Equipment 1'
    And Schedule '892-Test Schedule 1' is listed
    And Schedule '892-Test Schedule 1' has a 'Details' button
	When I press the 'Details' button
	Then I am on the 'Schedule Details' page
    And the 'Name' textbox displays '892-Test Schedule 1'
    And the 'Furnace Class' dropdown displays '-'
    And the 'Description' textbox displays 'Test Description 1'
    And the 'Schedule Type' radio button displays 'SAT'
    And the 'Frequency' dropdown displays 'None'
    And the 'Special Conditions' textbox displays 'Test Special Conditions 1'

@SingleSchedule
Scenario: Cancel from Schedule Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
	When I press the 'Cancel' button
	Then I am on the 'Equipment Details' page

@SingleSchedule
Scenario: Save empty form from Schedule Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I have cleared the 'Name' textbox
    And I have selected 'Please Select ...' in the 'Furnace Class' dropdown
    And I have selected 'Please Select ...' in the 'Frequency' dropdown
	When I press the 'Save' button
	Then I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I am shown the message "Please enter the name for your schedule"
    And I am shown the message "Please select a Furnace Class"
    And I am shown the message 'Please select a Frequency'

@SingleSchedule
Scenario: Save form from Schedule Details page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
    And I have selected '-' in the 'Furnace Class' dropdown
    And I have selected 'None' in the 'Frequency' dropdown
    And I have selected 'SAT' in the 'Schedule Type' radio button
	When I press the 'Save' button
	Then I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I am shown the message "Maximum Length is 120 Characters"

@MultipleSchedule
Scenario: Save form from Schedule Details page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I have entered '892-Test Schedule 2' in the 'Name' textbox
    And I have selected '5' in the 'Furnace Class' dropdown
    And I have selected 'Yearly' in the 'Frequency' dropdown
    And I have selected 'TUS' in the 'Schedule Type' radio button
	When I press the 'Save' button
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I am shown the message "A Schedule with that Name already exists for this Equipment"

@MultipleSchedule
Scenario: Save form from Schedule Details page with existing Schedule for Class and Type
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I have selected '1' in the 'Furnace Class' dropdown
    And I have selected 'None' in the 'Frequency' dropdown
    And I have selected 'SAT' in the 'Schedule Type' radio button
	When I press the 'Save' button
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I am shown the message "A Schedule of that Type & Furnace Class already exists for this Equipment"

@SingleSchedule
Scenario: Save complete form from Schedule Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Schedule Details' page for customer '892-Test Customer 1', site '892-Test Site 1', plant area '892-Test Plant Area 1', equipment '892-Test Equipment 1' and Schedule '892-Test Schedule 1'
    And I have entered '892-Test Schedule' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Equipment Details' page
    And I am shown the message "Schedule 892-Test Schedule saved successfully"


