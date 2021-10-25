@StoryId_419
Feature: 419-EditPlantArea
    As a Service Provider User
    I want to edit a Plant Area 
    So that I can change meta-data


@SinglePlantArea
Scenario: Cancel from Plant Area Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
	When I press the 'Cancel' button
	Then I am on the 'Site Details' page for '419-Test Site 1'

@SinglePlantArea
Scenario: Save empty form from Plant Area Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I have cleared the 'Name' textbox
    And I have cleared the 'Description' textbox
	When I press the 'Save' button
	Then I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I am shown the message "Please enter the name for your plant area"
    And I am shown the message "Please enter the Description of your plant area"

@SinglePlantArea
Scenario: Save form from Plant Area Details page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I am shown the message "Maximum Length is 120 Characters"

@MultiplePlantArea
Scenario: Save form from Plant Area Details page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I have entered '420-Test Plant Area 2' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I am shown the message "A Plant Area with that name already exists"

@SinglePlantArea
Scenario: Save complete form from Plant Area Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I have entered 'Dummy Description 2' in the 'Description' textbox
	When I press the 'Save' button
	Then I am on the 'Plant Area Details' page for customer '419-Test Customer 1', site '419-Test Site 1' and plant area '419-Test Plant Area 1'
    And I am shown the message "Plant Area 416-Test Plant Area saved successfully"