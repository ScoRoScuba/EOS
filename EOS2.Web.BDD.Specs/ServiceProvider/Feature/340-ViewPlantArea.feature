@StoryId_340
Feature: 340-ViewPlantArea
    As a Service Provider User
    I want to see a list of plant areas within a selected site
    So that I can select one

Background: 
    Given I am signed in as a 'ServiceProvider' 'user'
     And A Plant Area exists

Scenario: View Plant Area List
    When I go to the Site Details page 
    Then A Plant Area is listed

Scenario: Select a Plant Area
     And I go to the Site Details page 
     And A Plant Area is listed with a 'Details' button
    When I click on the 'Details' button
    Then I am on the Plant Area Details page

Scenario: View Plant Area Details
     And I go to the Plant Area Details page
    Then I am shown the correct data