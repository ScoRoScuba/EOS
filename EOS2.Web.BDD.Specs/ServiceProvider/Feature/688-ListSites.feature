@StoryId_688
Feature: 688-ListSites
    As a Service Provider 
    I want to be able to see the list of my customers
    so that I can manage them

@SingleCustomer
Scenario: View List of Sites (Zero existing)
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customers' page 
	When I press the 'Details' button for '688-Test Customer 1'
	Then I am on the 'Customer Details' page
    And No 'sites' are listed

@SingleSite
Scenario: View List of Sites (one existing)
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customers' page 
	When I press the 'Details' button for '688-Test Customer 1'
	Then I am on the 'Customer Details' page
    And A 'Site' with 'Name' of '688-Test Site 1' is listed
  