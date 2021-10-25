@StoryId_420
Feature: EditSite
    As a Service Provider
    I want to be able to Edit a Customers Site 
    So that I can ensure its details are always correct

@SingleSite
Scenario: Get to Site Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '420-Test Customer 1'
    And Site '420-Test Site 1' is listed
    And Site '420-Test Site 1' has a 'Details' button
	When I press the 'Details' button
	Then I am on the 'Site Details' page
    And the 'Name' textbox displays '420-Test Site 1'
    And the 'Address' textbox displays 'Dummy Address'
    And the 'Post Code' textbox displays 'BN13 3PL'

@SingleSite
Scenario: Cancel from Site Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Site Details' page for '420-Test Site 1'
	When I press the 'Cancel' button
	Then I am on the 'Customer Details' page

@SingleSite
Scenario: Save empty form from Site Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Site Details' page for '420-Test Site 1'
    And I have cleared the 'Name' textbox
    And I have cleared the 'Address' textbox
    And I have cleared the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'Site Details' page for '420-Test Site 1'
    And I am shown the message "Please enter the name for your site"
    And I am shown the message "Please enter the Post Code of your site"

@SingleSite
Scenario: Save form from Site Details page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Site Details' page for '420-Test Site 1'
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Site Details' page for '420-Test Site 1'
    And I am shown the message "Maximum Length is 120 Characters"

@MultipleSitesSingleCustomer
Scenario: Save form from Site Details page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Site Details' page for '420-Test Site 1'
    And I have entered '420-Test Site 2' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Site Details' page for '420-Test Site 1'
    And I am shown the message "A site with this name already Exists"

@SingleCustomer
Scenario: Save complete form from New Site page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Site' page
    And I have entered '420-Test Site' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'Site Details' page
    And I am shown the message "Site 416-Test Site saved successfully"
