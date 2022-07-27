using M2c.Domain;
using Xunit;

namespace Mc2.Test
{
    public class MobileValidatorTests
    {
        [Theory]
        [InlineData("+989121234567", true)]
        [InlineData("+31612345678", true)]
        [InlineData("+982188776655", false)]
        [InlineData("+312088887777", false)]
        public void MobileValidationTest_WithExpectedResult(string mobileNumber, bool expectedResult)
        {
            bool testResult = MobileValidator.IsValidNumber(mobileNumber);
            
            Assert.Equal(expectedResult, testResult);
        }
    }
}