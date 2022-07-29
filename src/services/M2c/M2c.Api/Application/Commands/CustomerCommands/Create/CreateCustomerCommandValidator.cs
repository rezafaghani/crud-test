using System.Text.RegularExpressions;
using FluentValidation;
using M2c.Domain;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is mandatory");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is mandatory");
            RuleFor(r => r.DateOfBirth).NotEmpty().WithMessage("Date of birth is mandatory");
            RuleFor(r => r.Email).EmailAddress().WithMessage("Valid email is required ");
            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is mandatory")
                .Must(MobileValidator.IsValidNumber).WithMessage("Phone number is not valid")
                .MaximumLength(15).WithMessage("Phone number can not more than 15 character");

            RuleFor(x => x.BankAccountNumber).Must(BeValidBankAccount)
                .WithMessage("Please specify a valid bank account");
        }

        private bool BeValidBankAccount(string account)
        {
            return Regex.IsMatch(account, @"^\d+$");
        }
    }
}