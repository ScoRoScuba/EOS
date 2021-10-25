@CreateServiceProviderOrganization
Feature: 597-CreateServiceProviderOrganization
	As a Portal Agent
	I want to be able to create a Service Provider Organisation
	so that I can administer who can provide Services

Scenario: Adds a new Service Provider Organization
	Given I am logged in as the Portal Agent 'keith.kraylic' with the password '!12345678A'
	And I am on the Service Provider Index Page
	And I am on the Service Provider Index Page
	Then I am on the Service Provider Organization Page
	When I click on Add Service Provider button
	#And I enter Name of '597New Service Provider EU'
	#And I enter the Address of 'New street, New Place'
	#And I enter a Postal Code of 'NP13 1JK'
	Then I am on the Create Service Provider Organization Page
	When I set the name to '597New Service Provider EU' and the address to 'New street, New Place' and the Postal Code to 'NP13 1JK'
	When I click the Save Button
	Then I am shown the message "Service Provider Saved"

Scenario: Adds a duplicate Service Provider Organization
	Given I am logged in as the Portal Agent 'keith.kraylic' with the password '!12345678A'
	And I am on the Service Provider Index Page
	Then I am on the Service Provider Organization Page
	When I click on Add Service Provider button
	#And I am on the Service Provider Index Page
	#And I click on Add Service Provider button
	#When I am on the Create Organization Page
	#And I enter Name to '597Duplicate Service Provider'
	#And I enter the Address of 'Duplicate Street, Duplicate place'
	#And I enter a Postal Code of 'DP13 1JK'
	Then I am on the Create Service Provider Organization Page
	When I set the name to '597New Service Provider EU' and the address to 'New street, New Place' and the Postal Code to 'NP13 1JK'
	When I click the Save Button
	Then I am shown the message 'An organization with that name already Exists'

