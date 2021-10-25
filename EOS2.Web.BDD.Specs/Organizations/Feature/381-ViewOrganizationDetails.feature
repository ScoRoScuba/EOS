@CreateUniqueOrganizationsRoles
@ViewOrganizationDetails
Feature: 381-ViewOrganizationDetails
	As a EOS Owner
	I want to select an organization and view its details
	So that I can manage an organization

Scenario: View Portal Agent Details
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Portal Agent Index Page
    And Portal Agent '381Eurotherm BDD Portal Agent EU' is listed
    And Portal Agent '381Eurotherm BDD Portal Agent EU' has a 'Details' button
	When I press the 'Details' button
	Then I am shown the 'Portal Agent Details' page
    And the 'Name' textbox displays '381Eurotherm BDD Portal Agent EU'
    And the 'Address' textbox displays '123 the street, somewhere close in someplace'
    And the 'ZIP Code' textbox displays 'BN13 1JK'

Scenario: View Service Provider Details
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Service Provider Index Page
    And Portal Agent '381Eurotherm BDD Service Provider EU' is listed
    And Portal Agent '381Eurotherm BDD Service Provider EU' has a 'Details' button
	When I press the 'Details' button
	Then I am shown the 'Service Provider Details' page
    And the 'Name' textbox displays '381Eurotherm BDD Service Provider EU'
    And the 'Address' textbox displays '123 the street, somewhere close in someplace'
    And the 'ZIP Code' textbox displays 'BN13 1JK'

Scenario: View Customer Details
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Customer Organizations page
    And Portal Agent '381Eurotherm BDD Customer EU' is listed
    And Portal Agent '381Eurotherm BDD Customer EU' has a 'Details' button
	When I press the 'Details' button
	Then I am shown the 'Customer Details' page
    And the 'Name' textbox displays '381Eurotherm BDD Customer EU'
    And the 'Address' textbox displays '123 the street, somewhere close in someplace'
    And the 'ZIP Code' textbox displays 'BN13 1JK'

