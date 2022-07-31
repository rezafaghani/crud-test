Feature: Create Valid Customer
get info of a valid customer and create

    @mytag
    Scenario: Create Valid Customer
        When the valid customer created with
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber          |
          | AliReza      | Faghani  | 1987-02-21  | +989930911855 | rfaghani@live.com | IR490170000000301070097004 |
        Then the customer created successfully

    Scenario: Create Duplicated valid Customer
        When Duplicated valid customer created
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email              | BankAccountNumber          |
          | AmirReza      | Faghani  | 1988-02-21  | +989930911852 | rfaghani5@live.com | IR490170000000301070097005 |
          | amirreza      | faghani  | 1988-02-21  | +989930911852 | rfaghani5@live.com | IR490170000000301070097005 |
        Then Create duplicated customer unsuccessful

    Scenario: Create customer with duplicated first name and last name but different birth date
        When customer birth date is different
          | FirstName | LastName | DateOfBirth | PhoneNumber   | Email              | BankAccountNumber          |
          | hasan     | due      | 1990-02-21  | +989930911858 | rfaghani8@live.com | IR490170000000301070097008 |
          | john      | due      | 1989-02-22  | +989930911858 | rfaghani9@live.com | IR490170000000301070097009 |
        Then Create customer with different birth date is successful