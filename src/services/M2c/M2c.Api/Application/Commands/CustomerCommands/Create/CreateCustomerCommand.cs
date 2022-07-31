using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommand : IRequest<bool>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}