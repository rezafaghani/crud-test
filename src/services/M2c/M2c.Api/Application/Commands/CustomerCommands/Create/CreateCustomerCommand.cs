﻿using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommand : IRequest<bool>
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}