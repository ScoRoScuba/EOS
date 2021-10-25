@CreateCustomerToPlantAreaEquipment
Feature: 415-CreateEquipment
	As a Service Provider User
	I want to be able create equipment within a plant area
	So that I can represent my equipment in EOS

Scenario: Create a new Item of Equipment
	Given I am signed in as 'sam.morris' with the password '!12345678A'	
	And I am on the Customers Index Page
	When I select the 'Details' button for Customer 'Eurotherm EU Customer'
	Then I am shown Details page for  'Eurotherm EU Customer'
	When I select the 'Details' button for Site 'Main EU Site'
	Then I am shown Details for 'Main EU Site'
	When I select the 'Details' button for 'Main Production Area'
	Then I am shown the Details for 'Plant Main Production Area'
	When I select the 'Add Equipment' button
	Then I am shown the 'New Equipment' page
	When I set 'Name' to 'Master Furnace'
	And I set 'Description' to 'Main Furnance of the area'
	And I set 'Make' to 'A Furnace Make'
	And I set 'Model; to 'A Furnace Model'
	And I set 'Serial Number' to '1234567890abcd'
	And I set 'Equipment Type' to 'HIP Furnace'
	And I set the 'Notes' to 'BDD Test Furnace'
	When I click the 'Save' button
	Then I am shown the message 'Equipment Master Furnace saved successfully'
	And  The Title has changed to 'Equipment Details - Master Furnace'
	And I am able to Add 'Schedules'

Scenario: Create a Duplicate Item of Equipment
	Given I am signed in as 'sam.morris' with the password '!12345678A'	
	And I am on the Customers Index Page
	When I select the 'Details' button for Customer 'Eurotherm EU Customer'
	Then I am shown Details page for  'Eurotherm EU Customer'
	When I select the 'Details' button for Site 'Main EU Site'
	Then I am shown Details for 'Main EU Site'
	When I select the 'Details' button for 'Main Production Area'
	Then I am shown the Details for 'Plant Main Production Area'
	When I select the 'Add Equipment' button
	Then I am shown the 'New Equipment' page
	When I set 'Name' to 'BDD Duplicate Tester'
	And I set 'Description' to 'Main Furnance of the area'
	And I set 'Make' to 'A Furnace Make'
	And I set 'Model; to 'A Furnace Model'
	And I set 'Serial Number' to '1234567890abcd'
	And I set 'Equipment Type' to 'HIP Furnace'
	And I set the 'Notes' to 'BDD Test Furnace'
	When I click the 'Save' button
	Then I am shown the message 'A piece of Equipment with that Name already exists for this Plant Area.' under the 'Name' field

Scenario: Edit an Item of Equipment
	Given I am signed in as 'sam.morris' with the password '!12345678A'	
	And I am on the Customers Index Page
	When I select the 'Details' button for Customer 'Eurotherm EU Customer'
	Then I am shown Details page for  'Eurotherm EU Customer'
	When I select the 'Details' button for Site 'Main EU Site'
	Then I am shown Details for 'Main EU Site'
	When I select the 'Details' button for 'Main Production Area'
	Then I am shown the Details for 'Plant Main Production Area'
	When I select the 'Details' button for 'BDD Editable Tester'	
	Then I am shown the Details page for 'BDD Editable Tester' 
	And The title says the 'Equipment Details - BDD Editable Tester'
	When I edit the 'Description' to 'Main Furnance of the area in the Main site'
	When I click the 'Save' button
	Then I am shown the message 'Equipment BDD Editable Tester saved successfully'




