@OrganizationUserCreatesUserAccount
@CreateNewUserAccount
Feature: 389-CreateNewUserAccount
	As a any organization administrator
	I want to add a new user to my organization and define their permissions
	So that they have access to my organization's data on EOS

Scenario: EOS Owner Adds New EOS Owner User 
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'NewEOSOwnerUser'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS Owner User'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"

Scenario: EOS Owner Adds Duplicate EOS Owner User
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'steve.sprott'
	And I set the Name to 'New'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "Name steve.sprott is already taken." under the User Name 

Scenario: EOS Owner Attempts to add EOS Owner User without username
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "Please enter a log in UserName for the user" under the User Name 

Scenario: EOS Owner Attempts to add EOS Owner User without Name
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'NewEOSOwnerUser2'
	And I set the FamilyName to 'EOS Owner User'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "Please enter a name for the user" under the Name 

Scenario: EOS Owner Attempts to add EOS Owner User without email address
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'NewEOSOwnerUser3'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS Owner User'
	When I click the Save Button
	#Then I am shown the message 'Please enter an email address' under the 'Email Address'
	Then I am shown the message 'Please enter an email address'
	When I set the Email Address to 'new.eos@owner.com'
	When I click the Save Button
    #And I am shown the message 'Please enter an email address' under the 'Confirmation Email Address'
   Then I am shown the message 'Please enter an email address'


Scenario: EOS Owner Attempts to add EOS Owner User with out matching email address
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'NewEOSOwnerUser4'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS Owner User'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'another@owner.com'
	When I click the Save Button
	#Then I am shown the message 'Emails addresses do not match' under 'Confirmation Email Address' 
	Then I am shown the message 'Emails addresses do not match' 

Scenario: Portal Agent Adds New Portal Agent User 
	Given I am logged in as the Portal Agent 'keith kraylic' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'NewPortalAgent'
	And I set the Name to 'New'
	And I set the FamilyName to 'Portal Agent User'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"
		
Scenario: Service Provider Adds New Service Provider User 
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page for Service Provider
	When I click on the Add User Button
	Then I am shown the New Users Page for Service Provider
	When I set the User Name to 'NewServiceProviderUser'
	And I set the Name to 'New'
	And I set the FamilyName to 'Service Provider User'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"

Scenario: Customer Adds New Customer User 
	Given I am signed in as 'damian.brett' with the password '!12345678A'
	And I am on the Organizations Home Page
	Then I am taken to the Organizations Page for Customer 
	When I click on the Add User Button
	Then I am shown the New Users Page for Customer
	When I set the User Name to 'NewCustomerUser'
	And I set the Name to 'New'
	And I set the FamilyName to 'Customer User'
	And I set the Email Address to 'new.eos@owner.com'
	And I set the Comparison Email Address to 'new.eos@owner.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"