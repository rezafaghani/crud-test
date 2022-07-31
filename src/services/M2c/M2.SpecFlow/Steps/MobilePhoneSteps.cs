using M2c.Api.Application.Commands.CustomerCommands.Create;
using M2c.Domain;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace M2.SpecFlow.Steps;

[Binding]
public sealed class MobilePhoneSteps
{
    private string validMobile;
    private string unValidMobile;
    private string notMobile;

    [When(@"Mobile phone is valid")]
    public void WhenMobilePhoneIsValid(Table table)
    {
        var rows = table.CreateSet<CreateCustomerCommand>();
        validMobile = rows.First().PhoneNumber;
    }

    [Then(@"mobile result is true")]
    public void ThenMobileResultIsTrue()
    {
        var result = MobileValidator.IsValidNumber(validMobile);
        Assert.True(result);
    }

    [When(@"phone number is not mobile")]
    public void WhenPhoneNumberIsNotMobile(Table table)
    {
        var rows = table.CreateSet<CreateCustomerCommand>();
        notMobile= rows.First().PhoneNumber;
    }

    [Then(@"mobile result is false")]
    public void ThenMobileResultIsFalse()
    {
        var result = MobileValidator.IsValidNumber(notMobile);
        Assert.False(result);
    }

    [When(@"mobile number is not valid")]
    public void WhenMobileNumberIsNotValid(Table table)
    {
        var rows = table.CreateSet<CreateCustomerCommand>();
        unValidMobile= rows.First().PhoneNumber;
    }

    [Then(@"result is false")]
    public void ThenResultIsFalse()
    {
        var result = MobileValidator.IsValidNumber(unValidMobile);
        Assert.False(result);
    }
}