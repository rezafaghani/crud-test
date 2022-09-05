using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace M2c.Api.Application.Commands.CustomerCommands.Update
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        [Required] public string Firstname { get; set; }

        [Required] public string Lastname { get; set; }

        [Required]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}