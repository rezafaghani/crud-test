namespace M2.SpecFlow.Steps;

[Binding]
public class CreateCustomerSteps
{
    [Given(@"a customer repository with the following customers:")]
    public void GivenACustomerRepositoryWithTheFollowingCustomers(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"John Michaels attempts to register with the first name ""(.*)"" , last name ""(.*)"" and email ""(.*)""")]
    public void WhenJohnMichaelsAttemptsToRegisterWithTheFirstNameLastNameAndEmail(string john, string michaels,
        string p2)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the customer repository should contain the following customers:")]
    public void ThenTheCustomerRepositoryShouldContainTheFollowingCustomers(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(
        @"Mike Smith attempts to register with the first name ""(.*)"" and last name ""(.*)"", birth date ""(.*)"" and email ""(.*)""")]
    public void WhenMikeSmithAttemptsToRegisterWithTheFirstNameAndLastNameBirthDateAndEmail(string john,
        string michaels, string p2, string p3)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the customer repository should contain the following users:")]
    public void ThenTheCustomerRepositoryShouldContainTheFollowingUsers(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"The mobile number should be a vlid mobile number")]
    public void WhenTheMobileNumberShouldBeAVlidMobileNumber()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the user repository should contain the following users:")]
    public void ThenTheUserRepositoryShouldContainTheFollowingUsers(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"Steve James attempts to register with the account number ""(.*)""")]
    public void WhenSteveJamesAttemptsToRegisterWithTheAccountNumber(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the registration should fail with ""(.*)""")]
    public void ThenTheRegistrationShouldFailWith(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the customer repository should contain the following accounts:")]
    public void ThenTheCustomerRepositoryShouldContainTheFollowingAccounts(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"Mike Smith attempts to register with the first name ""(.*)""  and email ""(.*)""")]
    public void WhenMikeSmithAttemptsToRegisterWithTheFirstNameAndEmail(string mike, string p1)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"The mobile number should be unique")]
    public void WhenTheMobileNumberShouldBeUnique()
    {
        ScenarioContext.StepIsPending();
    }
}