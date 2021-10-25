@Internationalization
Feature: 698-Internationalization
    As a User
    I want to see EOS in my own language
    So that I can understand and use EOS

Scenario: Correctly choose localization based on browser settings
	Given my browser is set to prefer 'Afrikaans'
	When EOS language selection is '|Auto|'
	Then the Eurotherm Online Services title should be surrounded by vertical bars

Scenario: Set the locale manually
    Given my browser is set to prefer 'English (United Kingdom)'
    And EOS language selection is 'Auto'
    And the Eurotherm Online Service title is not surrounded by vertical bars
    When I choose 'Afrikaans' from the language selector and click Ok
    Then the Eurotherm Online Services title should be surrounded by vertical bars

Scenario: Switch between UK and US English
    Given my browser is set to prefer 'English (United Kingdom)'
    And EOS language selection is 'Auto'
    And I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the Customer Organizations page
    And the organization table heading is 'Organisation Types'
    When I choose 'English (United States)' from the language selector on the Customer Organizations page and click Ok
    Then the organization table heading is 'Organization Types'
    When I choose 'English (United Kingdom)' from the language selector on the Customer Organizations page and click Ok
    Then the organization table heading is 'Organisation Types'

Scenario: POST request retains language selection
    Given my browser is set to prefer 'English (United Kingdom)'
    And EOS language selection is 'Auto'
    And I am signed in as 'sam.morris' with the password '!12345678A'
    And I am on the Customer Organizations page
    And the organization table heading is 'Organisation Types'
    When I choose 'English (United States)' from the language selector on the Customer Organizations page and click Ok
    And I click the Add Customer Organization button
    And I set the name to 'Widgets R Us' and the address to '1 The Street' and the Postal Code to 'XY13 KH12'
    And I click the Save Button
    And I click the'Customers'link
    Then the organization table heading is 'Organization Types'
 