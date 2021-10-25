@CreatePortalAgentOrganization
Feature: 598-CreatePortalAgentOrganization
	As an EOS Owner
	I want to be able to create Portal Agent Organisations
	so that I can manage who can manage Service Providers

Scenario: Adds a new Portal Agent Organization without a name
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Portal Agent Index Page
	Then I am on the Portal Agent Organization Page
	When I click on Add Portal Agent button
	Then I am on the Create Portal Agent Organization Page
	And I enter the Address of '123 the street, somewhere close in someplace'
	And I enter a Postal Code of 'BN13 1JK'
	When I click the Save Button
	Then I am shown the message 'Please enter a name for the Portal Agent' under the Name field

Scenario: Adds a new Portal Agent Organization
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Portal Agent Index Page
	And I am on the Portal Agent Index Page
	Then I am on the Portal Agent Organization Page
	When I click on Add Portal Agent button
	Then I am on the Create Portal Agent Organization Page
	#And I enter Portal Agent Name to '598Eurotherm EU'
	#And I enter the Address of '123 the street, somewhere close in someplace'
	#And I enter a Postal Code of 'BN13 1JK'
	When  I set the name to '598Eurotherm EU10' and the address to '123 the street, somewhere close in someplace' and the Postal Code to 'BN13 1JK'
	When I click the Save Button
	Then I am shown the message "Portal Agent saved"

Scenario: Adds a duplicate Portal Agent Organization
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	#And I click on Add Portal Agent button
	#When I am on the Create Portal Agent Organization Page
	And I am on the Portal Agent Index Page
	Then I am on the Portal Agent Organization Page
	When I click on Add Portal Agent button
	Then I am on the Create Portal Agent Organization Page
	#And I enter Portal Agent Name to '598Duplicate Eurotherm EU'
	#And I enter the Address of '123 the street, somewhere close in someplace'
	#And I enter a Postal Code of 'BN13 1JK'
	When  I set the name to '598Eurotherm EU' and the address to '123 the street, somewhere close in someplace' and the Postal Code to 'BN13 1JK'
	When I click the Save Button
	Then I am shown the message 'An organization with that name already Exists'

