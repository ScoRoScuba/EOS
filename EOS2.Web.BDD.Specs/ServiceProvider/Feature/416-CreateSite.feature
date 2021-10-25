@StoryId_416
Feature: CreateSite
    As a Service Provider
    I want to be able to Create a Site 
    So that I can add plant areas, equipment and instruments

@SingleCustomer
Scenario: Get to New Site page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '416-Test Customer 1'
	When I press the 'Add Site' button
	Then I am on the 'New Site' page

@SingleCustomer
Scenario: Cancel from New Site page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Site' page
	When I press the 'Cancel' button
	Then I am on the 'Customer Details' page

@SingleCustomer
Scenario: Save empty form from New Site page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Site' page
	When I press the 'Save' button
	Then I am on the 'New Site' page
    And I am shown the message "Please enter the name for your site"
    And I am shown the message "Please enter the Post Code of your main site"

@SingleCustomer
Scenario: Save form from New Site page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Site' page
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'New Site' page
    And I am shown the message "Maximum Length is 120 Characters"

@SingleSite
Scenario: Save form from New Site page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Site' page
    And I have entered '416-Test Site 1' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'New Site' page
    And I am shown the message "A site with this name already Exists"

@SingleCustomer
Scenario: Save complete form from New Site page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Site' page
    And I have entered '416-Test Site' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'Site Details' page
    And I am shown the message "Site 416-Test Site saved successfully"
