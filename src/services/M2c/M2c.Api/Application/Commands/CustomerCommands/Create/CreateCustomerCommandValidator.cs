using System;
using System.Globalization;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommandValidator :AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.Firstname).NotEmpty().WithMessage("First name is mandatory");
            RuleFor(r => r.Lastname).NotEmpty().WithMessage("Last name is mandatory");
            RuleFor(r => r.DateOfBirth).NotEmpty().WithMessage("Date of birth is mandatory");
            RuleFor(r => r.Email).Must(BeValidEmail).WithMessage("Valid email is required ");
            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is mandatory").MaximumLength(15).WithMessage("Phone number shouldn't be much more than 15 character");
            
            RuleFor(x => x.BankAccountNumber).Must(BeValidBankAccount).WithMessage("Please specify a valid bank account");
        }
        private bool BeValidBankAccount(string account)
        {
            return (Regex.IsMatch(account, @"^\d+$")) ;
        }

        private bool BeValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}