@StoryId_599
Feature: 599-CreateCustomerOrganization
    As a Service Provider
    I want to be able to create Customer Organisations
    so that I can manage who will consume my Services


Scenario: Get to New Customer Organization page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Organizations' page
	When I press the 'Add Customer Organization' button
	Then I am on the 'New Customer Organization' page

Scenario: Cancel from New Customer Organization page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Customer Organization' page
	When I press the 'Cancel' button
	Then I am on the 'Customer Organizations' page

Scenario: Save empty form from New Customer Organization page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Customer Organization' page
	When I press the 'Save' button
	Then I am on the 'New Customer Organization' page
    And I am shown the message "Please enter the name for your company"
    And I am shown the message "Please enter the Post Code of your main site"

Scenario: Save form from New Customer Organization page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Customer Organization' page
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'New Customer Organization' page
    And I am shown the message "Maximum Length is 120 Characters"

@MultipleCustomers
Scenario: Save form from New Customer Organization page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Customer Organization' page
    And I have entered '599-Test Customer 1' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'New Customer Organization' page
    And I am shown the message "An organization with that name already Exists"

Scenario: Save complete form from New Customer Organization page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Customer Organization' page
    And I have entered '599-Test Customer' in the 'Name' textbox
    And I have entered 'Faraday Close' in the 'Address' textbox
    And I have entered 'BN13 3PL' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'Customer Organization Details' page
    And I am shown the message "Customer Saved"

