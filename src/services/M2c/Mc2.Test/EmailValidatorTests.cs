using M2c.Domain;
using Xunit;

namespace Mc2.Test
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("rezafaghani@live.com", true)]
        [InlineData("faghani.r.axon@gmail.com", true)]
        [InlineData("faghani.r@orchid.com", true)]
        [InlineData("faghani.r@orchid.xyz", true)]
        [InlineData("faghani.r@orchidpharmed.com.", false)]
        public void EmailValidationTest_WithExpectedResult(string email, bool expectedResult)
        {
            bool testResult = EmailValidator.IsValidAsync(email);

            Assert.Equal(expectedResult, testResult);
        }
    }
}