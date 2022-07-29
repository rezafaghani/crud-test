Feature: Register Customer

    Background:

        Given a customer repository with the following customers:
          | firstname | lastname | email           | birthDate  | mobileNumber  | accountNumber
          | Mike      | Smith    | mike@gmail.com  | 2000-01-01 | +989930911852 | 0301070097003
          | Steve     | James    | steve@yahoo.com | 1986-02-21 | +989111704858 | 0301070097004
          | John      | Michaels | johnm@gmail.com | 1986-03-21 | +989111704859 | 0301070097005

    Scenario: Customers with unique data should be able to register

        When John Michaels attempts to register with the first name "john" , last name "Michaels" and email "johnm@gmail.com"
        Then the customer repository should contain the following customers:
          | firstname | lastname | email           | birthDate  | mobileNumber  | accountNumber
          | Mike      | Smith    | mike@gmail.com  | 2000-01-01 | +989930911852 | 0301070097003
          | Steve     | James    | steve@yahoo.com | 1986-02-21 | +989111704858 | 0301070097004
          | John      | Michaels | johnm@gmail.com | 1986-03-21 | +989111704859 | 0301070097005

    Scenario: Personal data include first name, last name, birth date, mobile number  have to be unique

        When Mike Smith attempts to register with the first name "john" and last name "Michaels", birth date "1986-03-21" and email "johnm@gmail.com"
        Then the customer repository should contain the following customers:
          | firstname | lastname | email           | birthDate  | mobileNumber  | accountNumber
          | Mike      | Smith    | mike@gmail.com  | 2000-01-01 | +989930911852 | 0301070097003
          | Steve     | James    | steve@yahoo.com | 1986-02-21 | +989111704858 | 0301070097004
          | John      | Michaels | johnm@gmail.com | 1986-03-21 | +989111704859 | 0301070097005

    Scenario: Mobile number have to be valid mobile number

        When The mobile number should be a vlid mobile number
        Then the customer repository should contain the following customers:
          | firstname | lastname | email           | birthDate  | mobileNumber  | accountNumber
          | Mike      | Smith    | mike@gmail.com  | 2000-01-01 | +989930911852 | 0301070097003
          | Steve     | James    | steve@yahoo.com | 1986-02-21 | +989111704858 | 0301070097004
          | John      | Michaels | johnm@gmail.com | 1986-03-21 | +989111704859 | 0301070097005

    Scenario: Mobile number have to be valid unique

        When The mobile number should be unique
        Then the customer repository should contain the following customers:
          | firstname | lastname | email           | birthDate  | mobileNumber  | accountNumber
          | Mike      | Smith    | mike@gmail.com  | 2000-01-01 | +989930911852 | 0301070097003
          | Steve     | James    | steve@yahoo.com | 1986-02-21 | +989111704858 | 0301070097004
          | John      | Michaels | johnm@gmail.com | 1986-03-21 | +989111704859 | 0301070097005

    Scenario Outline: Account number should be unique

        When Steve James attempts to register with the account number "<requested accountnumber>"
        Then the registration should fail with "AccountNumber taken"
        And the customer repository should contain the following accounts:
          | firstname | lastname | email           | birthDate  | mobileNumber  | accountNumber
          | Mike      | Smith    | mike@gmail.com  | 2000-01-01 | +989930911852 | 0301070097003
          | Steve     | James    | steve@yahoo.com | 1986-02-21 | +989111704858 | 0301070097004
          | John      | Michaels | johnm@gmail.com | 1986-03-21 | +989111704859 | 0301070097005

    Scenario Outline: Duplicated emails should be disallowed

        When Mike Smith attempts to register with the first name "mike"  and email "<email>"
        Then the registration should fail with "Email already registered"

        Examples: uppercase/lowercase aliases should be detected

          | comment                                         | email          |
          | emails should be case insensitive               | Mike@gmail.com |
          | domains should be case insensitive              | mike@Gmail.com |
          | unicode normalisation tricks should be detected | mᴵke@gmail.com |

        Examples: Gmail aliases should be detected

        GMail is a very popular system so many users will register
        with gmail emails. Sometimes they use gmail aliases or labels,
        to prevent users mistakenly registering for multiple accounts
        the user repository should recognise common Gmail tricks.

          | comment                                                    | email               |
          | google ignores dots in an email, so mi.ke is equal to mike | mi.ke@gmail.com     |
          | google allows setting labels by adding +label to an email  | mike+test@gmail.com |
          | googlemail is a equivalent alias for gmail                 | mike@googlemail.com |