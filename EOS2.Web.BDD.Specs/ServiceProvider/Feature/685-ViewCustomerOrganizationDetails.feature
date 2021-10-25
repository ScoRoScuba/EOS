@StoryId_685
Feature: 685-ViewCustomerOrganizationDetails
    As a Service Provider
    I want to be able to view the details of a customer organization
    So that I can view site, plant and calibration related information of the customer

@SingleCustomer
Scenario: Select Customer Organization to View
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customers' page
    And Customer '685-Test Customer 1' is listed
    And Customer '685-Test Customer 1' has a 'Details' button
	When I press the 'Details' button
	Then I am on the 'Customer Details' page
    And the 'Name' textbox displays '685-Test Customer 1'
    And the 'Address' textbox displays 'Dummy Address'
    And the 'Post Code' textbox displays 'BN13 3PL'

