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
                    // Replace 00 at beginning with +
                    telephoneNumber = "+" + telephoneNumber.Remove(0, 2);
                PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, "");
                //check if its a valid  phone number
                bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber);
                if (isValidNumber)

                {
                    //check if its a valid mobile number
                    PhoneNumberType phoneNumberType = phoneUtil.GetNumberType(phoneNumber);
                    return phoneNumberType == PhoneNumberType.MOBILE;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}