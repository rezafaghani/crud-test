namespace M2.SpecFlow.Steps;

[Binding]
public class CreateCustomerSteps
{
    [When(@"the valid customer created with")]
    public void WhenTheValidCustomerCreatedWith(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the customer created successfully")]
    public void ThenTheCustomerCreatedSuccessfully()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"Duplicated valid customer created")]
    public void WhenDuplicatedValidCustomerCreated(Table table)
    {
        ScenarioContext.StepIsPending();
    }


    [Then(@"Create duplicated customer unsuccessful")]
    public void ThenCreateDuplicatedCustomerUnsuccessful()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"customer birth date is different")]
    public void WhenCustomerBirthDateIsDifferent(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"Create customer with different birth date is successful")]
    public void ThenCreateCustomerWithDifferentBirthDateIsSuccessful()
    {
        ScenarioContext.StepIsPending();
    }

}