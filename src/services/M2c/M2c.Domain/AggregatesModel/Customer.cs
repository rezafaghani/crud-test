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
        //public MobileNumber PhoneNumber { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    /// <summary>
    /// ENUM 164
    /// https://en.wikipedia.org/wiki/Telephone_number_mapping
    /// </summary>
    public class MobileNumber
    {
        [Obsolete("Reserved for System", true)]
        public MobileNumber(string value)
        {
            Value = ulong.Parse(value.Trim('+'));
            if(!MobileValidator.IsValidNumber("value"))
            {
                throw new ArgumentException("Mobile number is invalid" + value);
            }
        }
        public MobileNumber(ulong value)
        {
            Value = value;
            
        }
        
        public ulong Value { get; set; }

        public override string ToString()
        {
            return $"+{Value}";
        }
    }
}