@OrganizationUsersCreateOrganizations
@CreateAnOrganization
Feature: 537-CreateAnOrganization
	As a Service Provider, Portal Agent or EOS Owner
	I want to create an Organization
	So that that Organization can have users added for accessing EOS

Scenario: EOS Owner can create Service Provider
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Service Provider Index Page
	Then I am on the Service Provider Organization Page
	When I click on Add Service Provider button
	Then I am on the Create Service Provider Organization Page
	#And I enter Name of '537New EOS Owner Service Provider EU'
	#And I enter the Address of '123 the street, somewhere close in someplace'
	#And I enter a Postal Code of 'BN13 1JK'
	When I set the name to '537New EOS Owner Service Provider EU11' and the address to '123 the street, somewhere close in someplace' and the Postal Code to 'BN13 1JK'
	When I click the Save Button
	Then I am shown the message "Service Provider Saved"

Scenario: EOS Owner can create Customer
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Customer Organizations page
	#And I enter Name of '537EOS Owners Customer'
	#And I enter the Address of '123 the street, somewhere close in someplace'
	#And I enter a Postal Code of 'BN13 1JK'
	When I click the Add Customer Organization button
	When I set the name to '537EOS Owners Customer' and the address to '123 the street, somewhere close in someplace' and the Postal Code to 'BN13 1JK'
	When I click the Save Button
	Then I am shown the message "Customer saved"

