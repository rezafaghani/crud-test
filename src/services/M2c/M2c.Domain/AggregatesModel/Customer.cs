using System;
using System.ComponentModel.DataAnnotations;
using M2c.Domain.SeedWork;

namespace M2c.Domain.AggregatesModel
{
    public class Customer : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}