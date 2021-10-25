@StoryId_694
Feature: 694-SelectPlantArea
    As a Site User or a Service Provider User
    I want to select a plant area from a list
    So that I can view its contents

@SinglePlantArea
Scenario: Show plant areas on Sites page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Site Details' page for customer '694-Test Customer 1' and site '694-Test Site 1'
    And Plant Area '694-Test Plant Area 1' is listed
    And Plant Area '694-Test Plant Area 1' has a 'Details' button
	When I press the 'Details' button
	Then I am on the 'Plant Area Details' page for '694-Test Plant Area 1'
