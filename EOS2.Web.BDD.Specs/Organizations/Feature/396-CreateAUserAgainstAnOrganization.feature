@CreateOrganizationTreeForUserCreation
@CreateAUserAgainstAnOrganization
Feature: 396-CreateAUserAgainstAnOrganization
	As an EOS Owner or a Portal Agent or Service Provider Administrator
	I want to add a user to an organization below me within EOS
	So that that person can use EOS

Scenario: EOS Owner Creates Portal Agent User
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the EOS Owners Organizations Home Page
	When I click on the 'Portal Agents' menu option
	Then I am shown a list of Portal Agents
	When I click on the 'Manage Users' button for 'New Portal Agent Organization'
	Then I am shown 'Portal Agents Users' Page
	When I click on the Add User Button
	Then I am shown the New Users Page for Portal Agents
	When I set the User Name to 'NewEOSPortalAgentUser12367'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS PortalAgent User'
	And I set the Email Address to 'new.eos@PortalAgent.com'
	And I set the Comparison Email Address to 'new.eos@PortalAgent.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"
	When I click the 'Cancel' button
	Then I am shown 'Portal Agents Users' Page
	And I see the new User in the list
	And I see the button 'Set Password'
	When I click the 'Set Password'
	Then I am taken to the Set Password Page
	When I set the Password to 'NewPortalAgentPwd'
	And set the Confirmation Password to 'NewPortalAgentPwd' 
	When I click the Save Button
	Then I am shown the 'User Details' page
	And the UserName is set to 'NewEOSPortalAgentUser12367'
	Then I am shown the message "Password Updated successfully"
	When I click on 'Sign Out'
	Then I am shown the 'Home Page'
	When I click on 'Sign In'
	Then I am shown the 'Sign In' page
	#When I set the User Name to 'NewPortalAgentUser'
	#And I set the Password to 'NewPortalAgentPassword'
	#When I click on the 'Sign In' button
	When I am logged in as the Portal Agent User 'NewEOSPortalAgentUser12367' with the password 'NewPortalAgentPwd'
	Then I am shown the Portal Agents Organizations Home Page
	
Scenario: EOS Owner Creates Service Provider User
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the EOS Owners Organizations Home Page
	When I click on the 'Service Providers' menu option
	Then I am shown a list of Service Providers
	When I click on the 'Manage Users' button for 'New Service Provider Organization'
	Then I am shown 'New Service Provider Organization Users' Page
	When I click on the Add User Button
	Then I am shown the New Users Page for Service Provider User
	When I set the User Name to 'NewServiceProviderUser123456'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS ServiceProvider User'
	And I set the Email Address to 'new.eos@ServiceProvider.com'
	And I set the Comparison Email Address to 'new.eos@ServiceProvider.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"
	When I click the 'Cancel' button
	Then I am shown 'New Service Provider Organization Users' Page
	And I see the new User in the list
	And I see the button 'Set Password'
	When I click the 'Set Password'
	Then I am taken to the Set Password Page
	When I set the Password to 'NewServiceProvPwd'
	And set the Confirmation Password to 'NewServiceProvPwd' 
	When I click the Save Button
	Then I am shown the 'User Details For Service Provider' page
	Then I am shown the message "Password Updated successfully"
	And the UserName is set to 'NewServiceProviderUser123456'
	When I click on 'Sign Out'
	Then I am shown the 'Home Page'
	When I click on 'Sign In'
	Then I am shown the 'Sign In' page
	#When I set the UserName to 'NewServiceProviderUser'
	#And I set the Password to 'NewServiceProviderPassword'
	#When I click on the 'Sign In' button
	When I am logged in as the Service Provider User 'NewServiceProviderUser123456' with the password 'NewServiceProvPwd'
	Then I am shown the Service Providers Organizations Home Page

Scenario: EOS Owner Creates Customer User
	Given I am logged in as the EOS Owner 'steve.sprott' with the password '!12345678A'
	And I am on the Organizations Home Page
	When I click on the 'Customers' menu option
	Then I am shown a list of Customers
	When I click on the 'Manage Users' button for 'New Customer Organization'
	Then I am shown 'New Customer Organization Users' Page
	When I click on the Add User Button
	#Then I am shown the New Users Page
	Then I am shown the New Users Page for EOS Owner
	When I set the User Name to 'NewCustomerUser'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS Customer User'
	And I set the Email Address to 'new.eos@Customer.com'
	And I set the Comparison Email Address to 'new.eos@Customer.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"
	When I click the 'Cancel' button
	Then I am shown 'New Customer Organization Users' Page
	And I see the new User in the list
	And I see the button 'Set Password'
	When I click the 'Set Password'
	Then I am taken to the Set Password Page
	When I set the Password to 'NewCustomerPassword'
	And set the Confirmation Password to 'NewCustomerPassword' 
	When I click the Save Button
	Then I am shown the 'User Details For Customer' page
	Then I am shown the message "Password Updated successfully"
	And the UserName is set to 'NewCustomerUser'
	When I click on 'Sign Out'
	Then I am shown the 'Home Page'
	When I click on 'Sign In'
	Then I am shown the 'Sign In' page
	#When I set the UserName to 'NewCustomerUser'
	#And I set the Password to 'NewCustomerPassword'
	#When I click on the 'Sign In' button
	When I am logged in as the Customer User 'NewCustomerUser' with the password 'NewCustomerPassword'
	Then I am shown the Customer Organizations Home Page for 'New Customer Organization'

Scenario: Portal Agent Creates Service Provider User
	Given I am logged in as the Portal Agent 'keith.kraylic' with the password '!12345678A'
	And I am on the Portal Agents Organizations Home Page
	When I click on the 'Service Providers' menu option
	Then I am shown a list of Service Providers
	When I click on the 'Manage Users' button for 'New Portal Agent Service Provider Organization'
	Then I am shown 'New Service Provider Organization Users' Page
	When I click on the Add User Button
	Then I am shown the New Users Page for Portal Agent Service Provider User
	When I set the User Name to 'NewPAServiceProviderUser1234'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS ServiceProvider User'
	And I set the Email Address to 'new.eos@PAServiceProvider.com'
	And I set the Comparison Email Address to 'new.eos@PAServiceProvider.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"
	When I click the 'Cancel' button
	Then I am shown 'New Portal Agent Service Provider Organization Users' Page
	And I see the new User in the list
	And I see the button 'Set Password'
	When I click the 'Set Password'
	Then I am taken to the Set Password Page
	When I set the Password to 'NewPAServiceProPwd'
	And set the Confirmation Password to 'NewPAServiceProPwd' 
	When I click the Save Button
	Then I am shown the 'User Details For Service Provider' page
	Then I am shown the message "Password Updated successfully"
	And the UserName is set to 'NewPAServiceProviderUser1234'
	When I click on 'Sign Out'
	Then I am shown the 'Home Page'
	When I click on 'Sign In'
	Then I am shown the 'Sign In' page
	#When I set the UserName to 'NewPAServiceProviderUser'
	#And I set the Password to 'NewPAServiceProviderPassword'
	When I am logged in as the Service Provider User 'NewPAServiceProviderUser1234' with the password 'NewPAServiceProPwd'
	#When I click on the 'Sign In' button
	Then I am shown the Service Providers Organizations Home Page
	
Scenario: Service Provider Creates Customer User
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the Service Providers Organizations Home Page
	When I click on the 'Customers' menu option
	Then I am shown a list of Service Providers
	When I click on the 'Manage Users' button for 'New Customer Organization'
	Then I am shown 'New Customer Organization Users' Page
	When I click on the Add User Button
	Then I am shown the New Users Page
	When I set the User Name to 'NewSPCustomerUser'
	And I set the Name to 'New'
	And I set the FamilyName to 'EOS Customer User'
	And I set the Email Address to 'new.eos@PCustomer.com'
	And I set the Comparison Email Address to 'new.eos@Customer.com'
	When I click the Save Button
	Then I am shown the message "User saved successfully"
	When I click the 'Cancel' button
	Then I am shown 'New Customer Organization Users' Page
	And I see the new User in the list
	And I see the button 'Set Password'
	When I click the 'Set Password'
	Then I am taken to the Set Password Page
	When I set the Password to 'NewCustomerPassword'
	And set the Confirmation Password to 'NewCustomerPassword' 
	When I click the Save Button
	Then I am shown the 'Service Provider Customer User Details' page
	Then I am shown the message "Password Updated successfully"
	And the UserName is set to 'NewSPCustomerUser'
	When I click on 'Sign Out'
	Then I am shown the 'Home Page'
	When I click on 'Sign In'
	Then I am shown the 'Sign In' page
	#When I set the UserName to 'NewCustomerUser'
	#And I set the Password to 'NewCustomerPassword'
	When I am logged in as the Service Provider Cutomer User 'NewCustomerUser' with the password 'NewCustomerPassword'
	#When I click on the 'Sign In' button
	Then I am shown the Customers Organizations Home Page
