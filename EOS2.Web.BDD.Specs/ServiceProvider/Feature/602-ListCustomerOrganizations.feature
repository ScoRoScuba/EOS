@StoryId_602
Feature: 602-ListCustomerOrganizations
    As a Service Provider 
    I want to be able to see the list of my customers
    so that I can manage them

Scenario: View List of Customer Organizations (Zero existing)
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Manage' page
	When I press the 'Customers' link
	Then I am on the 'Customer Organizations' page
    And No customers are listed

@MultipleCustomers
Scenario: View List of Customer Organizations (three existing)
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Manage' page
    And '3' Customers exist in the system
	When I press the 'Customers' link
	Then I am on the 'Customer Organizations' page
    And A 'Customer' with 'Name' of '602-Test Customer 1' is listed
    And A 'Customer' with 'Name' of '602-Test Customer 2' is listed
    And A 'Customer' with 'Name' of '602-Test Customer 3' is listed
  