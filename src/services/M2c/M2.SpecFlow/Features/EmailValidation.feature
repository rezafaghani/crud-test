Feature: EmailValidation
check for mobile validations

    @email
    Scenario: Email is valid
        When email is valid
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber          |
          | Reza      | Faghani  | 1986-02-21  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
        Then Customer by valid email is created Successfully

    Scenario: Email is duplicated
        When email is duplicated
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber         |
          | Reza      | Faghani  | 1986-02-23  | +989930911852 | rfaghani91@live.com | IR490170000000301070097003 |
          | Ali       | Faghani  | 1986-02-24  | +989930911853 | rfaghani91@live.com | IR490170000000301070097003 |
        Then Customer with duplicated email not create