@StoryId_684
Feature: 684-EditCustomerOrganization
    As a Service Provider 
    I want to select an organization and edit it
    So that I can maintain the correct details for them

@SingleCustomer
Scenario: Cancel from Customer Organization Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '684-Test Customer 1'
	When I press the 'Cancel' button
	Then I am on the 'Customers' page

@SingleCustomer
Scenario: Save empty form from Customer Organization Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '684-Test Customer 1'
    And I have cleared the 'Name' textbox
    And I have cleared the 'Address' textbox
    And I have cleared the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'Customer Organization Details' page for '684-Test Customer 1'
    And I am shown the message "Please enter the name for your company"
    And I am shown the message "Please enter the Post Code of your main site"

@SingleCustomer
Scenario: Save form from Customer Organization Detail page with invalid name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '684-Test Customer 1'
    And I have entered '1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Customer Details' page for '684-Test Customer 1'
    And I am shown the message "Maximum Length is 120 Characters"

@MultipleCustomers
Scenario: Save form from Customer Organization Details page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '684-Test Customer 1'
    And I have entered '684-Test Customer 2' in the 'Name' textbox
	When I press the 'Save' button
	Then I am on the 'Customer Details' page for '684-Test Customer 1'
    And I am shown the message "An organization with that name already Exists"

@SingleCustomer
Scenario: Save complete form from Customer Organization Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Customer Details' page for '684-Test Customer 1'
    And I have entered '684-Test Customer' in the 'Name' textbox
    And I have entered 'Fleming Gardens' in the 'Address' textbox
    And I have entered 'BN01 1AA' in the 'Post Code' textbox
	When I press the 'Save' button
	Then I am on the 'Customer Details' page for '684-Test Customer'
    And I am shown the message "Customer Saved"
    And the 'Name' textbox displays '684-Test Customer'
    And the 'Address' textbox displays 'Fleming Gardens'
    And the 'Post Code' textbox displays 'BN01 1AA'
