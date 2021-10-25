@CreateCustomerToPlantAreaEquipment
Feature: 421-EditEquipment
As a Site Administrator or a Service Provider User
I want to be able edit equipment
So that I can change meta-data

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