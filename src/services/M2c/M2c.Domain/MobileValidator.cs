using PhoneNumbers;

namespace M2c.Domain
{
    public static class MobileValidator
    {
        public static bool IsValidNumber(string telephoneNumber)
        {
            try
            {
                telephoneNumber = telephoneNumber.Trim();
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                if (telephoneNumber.StartsWith("00"))
                {
                    // Replace 00 at beginning with +
                    telephoneNumber = "+" + telephoneNumber.Remove(0, 2);
                }
                PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, "");    

                bool result =phoneUtil.IsValidNumber(phoneNumber);
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}