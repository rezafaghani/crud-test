Feature: MobilePhone
Check if the mobile is valid or not

    @phonenumber
    Scenario: Validate mobile phone
        When Mobile phone is valid
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber          |
          | Reza      | Faghani  | 1986-02-21  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
        Then mobile result is true

    Scenario: Valid phone number but not mobile
        When phone number is not mobile
          | FirstName | LastName | DateOfBirth | PhoneNumber    | Email                | BankAccountNumber          |
          | Reza      | Faghani  | 1986-02-21  | +98a9930911852 | rezafaghani@live.com | IR490170000000301070097003 |
        Then mobile result is false

    Scenario: mobile number is not valid
        When mobile number is not valid
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | AccountNumber              |
          | Reza      | Faghani  | 1986-02-21  | +982122413142 | rezafaghani@live.com | IR490170000000301070097003 |
        Then result is false