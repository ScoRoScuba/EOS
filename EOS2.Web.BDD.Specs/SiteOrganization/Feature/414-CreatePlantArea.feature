@StoryId_414
@CreatePlantArea
Feature: 414-CreatePlantArea
    As a Site Administrator or a Service Provider User
    I want to create a Plant Area within a site
    So that I can represent the organization of my site

    Must not exist already at the Site
    Must have a Name/Unique Attribute within site
    Must have a description

@CreatePlantArea
Scenario: Create a new plant area
    Given that I am signed in as the service provider 'sam.morris' with the password '!12345678A'
    And that I have a customer organization '414 Widgets Associates'
    And that I have a site '414 Borough Road'
    When I create the plant area '414 Heat Treatment' description 'Contract heat treatment'
    Then '414 Widgets Associates' site '414 Borough Road' should list the plant area '414 Heat Treatment'

@CreatePlantArea
Scenario: Plant area name is too long
    Given that I am signed in as the service provider 'sam.morris' with the password '!12345678A'
    And that I have a customer organization '414 Widgets Associates'
    And that I have a site '414 Borough Road'
    When I create the plant area 'ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ' description 'Contract heat treatment'
    Then the 'Name' field should show the error 'Maximum Length is 120 Characters'

