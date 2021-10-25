@StoryId_343
Feature: 343-CalibrationCertificateEditView
    As a Service Provider User
    I want to view, download, and print the current calibration certificate for an instrument
    So that I can prove that the instrument is calibrated

@SingleCalibrationCertificate
Scenario: Get to Certificate Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Instrument Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1' and instrument '343-Test Instrument 1'
    And Certificate '343-Test Certificate 1' is listed
    And Certificate '343-Test Certificate 1' has a 'Details' button
	When I press the 'Details' button
	Then I am on the 'Certificate Details' page for '343-Test Certificate 1'
    And the 'Certificate Number' textbox displays '343-Test Certificate 1'
    And the 'Start Date' textbox displays '01/01/2014'
    And the 'End Date' textbox displays '01/12/2014'

@SingleCalibrationCertificate
Scenario: Cancel from Certificate Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
	When I press the 'Cancel' button
	Then I am on the 'Instrument Details' page for '343-Test Instrument 1'

@SingleCalibrationCertificate
Scenario: Save empty form from Certificate Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I have cleared the 'Certificate Number' textbox
    And I have cleared the 'Start Date' textbox
    And I have cleared the 'End Date' textbox
	When I press the 'Save' button
	Then I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I am shown the message "Please enter a Certificate Number"
    And I am shown the message "Start Date must be supplied"
    And I am shown the message 'End Date must be supplied'

@SingleCalibrationCertificate
Scenario: Save form from Certificate Details page with invalid Start Date & invalid End Date
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I have entered 'abcdef' in the 'Start Date' textbox
    And I have entered 'ghijkl' in the 'End Date' textbox
    When I press the 'Save' button
	Then I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I am shown the message "The value 'abcdef' is not valid for Start Date."
    And I am shown the message "The value 'ghijkl' is not valid for End Date."

@SingleCalibrationCertificate
Scenario: Save form from Certificate Details page with End Date less than Start Date
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I have entered '01/01/2014' in the 'Start Date' textbox
    And I have entered '01/12/2013' in the 'End Date' textbox
    When I press the 'Save' button
	Then I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I am shown the message "End Date must be after Start Date"

@MultipleCalibrationCertificate
Scenario: Save form from Certificate Details page with existing name
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I have entered '343-Test Certificate 2' in the 'Certificate Number' textbox
	When I press the 'Save' button
	Then I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I am shown the message "A certificate with that Certificate Number already exists for this customer"

@SingleCalibrationCertificate
Scenario: Save complete form from Schedule Details page
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I have entered '343-Test Certificate 2' in the 'Certificate Number' textbox
	When I press the 'Save' button
	Then I am on the 'Instrument Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1' and instrument '343-Test Instrument 1'
    And I am shown the message "Certificate 343-Test Certificate 2 saved successfully"

@SingleCalibrationCertificate
Scenario: Save complete form from Certificate Details page with new pdf
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And I have entered the location of a new pdf file in the 'Certificate' textbox
    When I press the 'Save' button
	Then I am on the 'Instrument Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1' and instrument '343-Test Instrument 1'
    And I am shown the message "Certificate 343-Test Certificate 1 saved successfully"
    And In the 'Certificates' panel there is a row for '349-Test Certificate 1'
    And certificate '349-Test Certificate 1' has a 'View PDF' button
    When I press the 'View PDF' button
    Then the new pdf file is downloaded

@SingleInstrument
Scenario: View pdf from Certificate Details page 
	Given I am signed in as 'sam.morris' with the password '!12345678A'
	And I am on the 'New Certificate Upload' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1' and instrument '343-Test Instrument 1'
    And I have entered '343-Test Certificate 1' in the 'Certificate Number' textbox 
    And I have entered '01/01/2014' in the 'Start Date' textbox
    And I have entered '01/12/2014' in the 'End Date' textbox
    And I have entered the location of a new pdf file in the 'Certificate' textbox
    When I press the 'Upload' button
	Then I am on the 'Instrument Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1' and instrument '343-Test Instrument 1'
    And I am shown the message "Certificate 343-Test Certificate 1 saved successfully"
    And In the 'Certificates' panel there is a row for '349-Test Certificate 1'
    And certificate '349-Test Certificate 1' has a 'Detail' button
    When I press the 'Detail' button
	Then I am on the 'Certificate Details' page for customer '343-Test Customer 1', site '343-Test Site 1', plant area '343-Test Plant Area 1', instrument '343-Test Instrument 1' and certificate '343-Test Certificate 1'
    And certificate '349-Test Certificate 1' has a 'View PDF' button
    When I press the 'View PDF' button
    Then the pdf file is downloaded
