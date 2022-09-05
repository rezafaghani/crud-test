using System.Net;
using System.Net.Http.Json;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace M2.SpecFlow.Steps;

[Binding]
public sealed class EmailSteps
{
    private CreateCustomerCommand validUserEmail;
    private List<CreateCustomerCommand> duplicatedEmailUser;
    private readonly HttpClient _httpClient;

    public EmailSteps(HttpClient httpClient)
    {
        _httpClient = httpClient;
        duplicatedEmailUser = new List<CreateCustomerCommand>();
        validUserEmail = new CreateCustomerCommand();
    }


    [When(@"email is valid")]
    public void WhenEmailIsValid(Table table)
    {
        var rows = table.CreateSet<CreateCustomerCommand>();
        validUserEmail = rows.First();
    }

    [Then(@"Customer by valid email is created Successfully")]
    public async Task ThenCustomerByValidEmailIsCreatedSuccessfully()
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", validUserEmail);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        Assert.True(result);
    }

    [When(@"email is duplicated")]
    public void WhenEmailIsDuplicated(Table table)
    {
        duplicatedEmailUser = new List<CreateCustomerCommand>();
        var rows = table.CreateSet<CreateCustomerCommand>();
        foreach (var item in rows)
        {
            duplicatedEmailUser.Add(item);
        }
    }

    [Then(@"Customer with duplicated email not create")]
    public async Task ThenCustomerWithDuplicatedEmailNotCreate()
    {
        var finalResult = true;
        var x = duplicatedEmailUser[0];
        var firstResponse = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", x);
        var firstResult = await firstResponse.Content.ReadFromJsonAsync<bool>();
        if (firstResponse.StatusCode != HttpStatusCode.OK)
        {
             finalResult = false;
        }

        
        var y = duplicatedEmailUser[1];
        var response = await _httpClient.PostAsJsonAsync("/api/v1/Customer/create", y);
        var result = await response.Content.ReadFromJsonAsync<object>();
        if (response.StatusCode == HttpStatusCode.BadRequest)
            finalResult = false;
        Assert.False(finalResult);
    }
}