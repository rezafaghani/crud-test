Feature: Create Valid Customer
get info of a valid customer and create

    @mytag
    Scenario: Create Valid Customer
        When the valid customer created with
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber          |
          | Reza      | Faghani  | 1986-02-21  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
        Then the customer created successfully

    Scenario: Create Duplicated valid Customer
        When Duplicated valid customer created
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber          |
          | Reza      | Faghani  | 1986-02-21  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
          | reza      | faghani  | 1986-02-21  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
        Then Create duplicated customer unsuccessful

    Scenario: Create customer with duplicated first name and last name but different birth date
        When customer birth date is different
          | FirstName | LastName | DateOfBirth | PhoneNumber | Email                  | BankAccountNumber |
          | Reza      | Faghani  | 1986-02-21  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
          | reza      | faghani  | 1986-02-22  | +989930911852 | rezafaghani@live.com | IR490170000000301070097003 |
        Then Create customer with different birth date is successful