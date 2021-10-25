@EOSOwnerEditAnOrganization
@EditAnOrganization
Feature: 387-EditAnOrganization
	As a EOS Owner
	I want to select an organization and edit it
	So that I can manage an organization

Scenario: Edit Portal Agent Details
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Portal Agent Index Page
    And Portal Agent '387Eurotherm BDD Portal Agent EU' is listed
    And Portal Agent '387Eurotherm BDD Portal Agent EU' has a 'Details' button
	When I press the 'Details' button
	Then I am shown the 'Portal Agent Details' page
    And the 'Name' textbox displays '387Eurotherm BDD Portal Agent EU'
    And the 'Address' textbox displays '123 the street, somewhere close in someplace'
    And the 'ZIP Code' textbox displays 'BN13 1JK'
	When I change the 'Address' textbox to '999 a changed address, somewhere new'
	When I click the Save Button
	Then I am shown the message "Portal Agent saved"
	And the 'Address' textbox displays '999 a changed address, somewhere new'

Scenario: Edit Service Provider Details
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Service Provider Index Page
    And Portal Agent '387Eurotherm BDD Service Provider EU' is listed
    And Portal Agent '387Eurotherm BDD Service Provider EU' has a 'Details' button
	When I press the 'Details' button
	Then I am shown the 'Service Provider Details' page
    And the 'Name' textbox displays '387Eurotherm BDD Service Provider EU'
    And the 'Address' textbox displays '123 the street, somewhere close in someplace'
    And the 'ZIP Code' textbox displays 'BN13 1JK'
	When I change the 'Address' textbox to '1000 a changed address, somewhere new'
	When I click the Save Button
	Then I am shown the message "Service Provider Saved"
	And the 'Address' textbox displays '1000 a changed address, somewhere new'

Scenario: Edit Customer Details
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Customer Organizations page
    And Portal Agent '387Eurotherm BDD Customer EU' is listed
    And Portal Agent '387Eurotherm BDD Customer EU' has a 'Details' button
	When I press the 'Details' button
	Then I am shown the 'Customer Details' page
    And the 'Name' textbox displays '387Eurotherm BDD Customer EU'
    And the 'Address' textbox displays '123 the street, somewhere close in someplace'
    And the 'ZIP Code' textbox displays 'BN13 1JK'
	When I change the 'Address' textbox to '1001 a changed address, somewhere new'
	When I click the Save Button
	Then I am shown the message "Customer saved"
	And the 'Address' textbox displays '1001 a changed address, somewhere new'
