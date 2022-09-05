using System.Net;
using System.Net.Http.Json;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace M2.SpecFlow.Steps;

[Binding]
public class CreateCustomerSteps
{
    private CreateCustomerCommand _validCustomer;
    private List<CreateCustomerCommand> _duplicatedCustomer;
    private List<CreateCustomerCommand> _duplicatedCustomerWithBirth;
    private readonly HttpClient _httpClient;

    public CreateCustomerSteps(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _validCustomer = new CreateCustomerCommand();
        _duplicatedCustomer = new List<CreateCustomerCommand>();
        _duplicatedCustomerWithBirth = new List<CreateCustomerCommand>();
    }

    [When(@"the valid customer created with")]
    public void WhenTheValidCustomerCreatedWith(Table table)
    {
        var rows = table.CreateSet<CreateCustomerCommand>();
        _validCustomer = rows.First();
    }

    [Then(@"the customer created successfully")]
    public async  Task ThenTheCustomerCreatedSuccessfully()
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", _validCustomer);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        Assert.True(result);
    }

    [When(@"Duplicated valid customer created")]
    public void WhenDuplicatedValidCustomerCreated(Table table)
    {
        _duplicatedCustomer = new List<CreateCustomerCommand>();
        var rows = table.CreateSet<CreateCustomerCommand>();
        foreach (var item in rows)
        {
            _duplicatedCustomer.Add(item);
        }
    }


    [Then(@"Create duplicated customer unsuccessful")]
    public async Task ThenCreateDuplicatedCustomerUnsuccessful()
    {
        var finalResult = true;
        var x = _duplicatedCustomer[0];
        var firstResponse = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", x);
        var firstResult = await firstResponse.Content.ReadFromJsonAsync<bool>();
        if (firstResponse.StatusCode != HttpStatusCode.OK)
        {
            finalResult = false;
        }

        
        var y = _duplicatedCustomer[1];
        var response = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", y);
        var result = await response.Content.ReadFromJsonAsync<object>();
        if (response.StatusCode == HttpStatusCode.BadRequest)
            finalResult = false;
        Assert.False(finalResult);
    }

    [When(@"customer birth date is different")]
    public void WhenCustomerBirthDateIsDifferent(Table table)
    {
        _duplicatedCustomerWithBirth = new List<CreateCustomerCommand>();
        var rows = table.CreateSet<CreateCustomerCommand>();
        foreach (var item in rows)
        {
            _duplicatedCustomerWithBirth.Add(item);
        }
    }

    [Then(@"Create customer with different birth date is successful")]
    public async Task ThenCreateCustomerWithDifferentBirthDateIsSuccessful()
    {
        var finalResult = true;
        var x = _duplicatedCustomerWithBirth[0];
        var firstResponse = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", x);
        var firstResult = await firstResponse.Content.ReadFromJsonAsync<bool>();
        if (firstResponse.StatusCode != HttpStatusCode.OK)
        {
            finalResult = true;
        }

        
        var y = _duplicatedCustomerWithBirth[1];
        var response = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", y);
        var result = await response.Content.ReadFromJsonAsync<object>();
        if (response.StatusCode == HttpStatusCode.BadRequest)
            finalResult = false;
        Assert.True(finalResult);
    }

}