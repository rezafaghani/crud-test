using BoDi;
using Microsoft.Extensions.Configuration;

namespace M2.SpecFlow.Hooks;

[Binding]
public class Hooks
{
    private readonly IObjectContainer _objectContainer;

    public Hooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    private static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }

    [BeforeTestRun()]
    public static void DockerComposeUp()
    {
        var config = LoadConfiguration();
        var dockerComposeFileName = config["DockerComposeFileName"];
        
        var filePath = Path.GetFullPath(dockerComposeFileName);
        ExecuteBash.Bash($"docker compose -f {filePath} up -d");
    }

    [AfterTestRun()]
    public static void DockerComposeDown()
    {
        var config = LoadConfiguration();
        var dockerComposeFileName = config["DockerComposeFileName"];
        ExecuteBash.Bash($"docker compose -f {dockerComposeFileName} down");
    }

    [BeforeScenario()]
    public void AddHttpClient()
    {
        var config = LoadConfiguration();
        var confirmationUrl = config["BaseAddress"];
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(confirmationUrl)
            
        };
        _objectContainer.RegisterInstanceAs(httpClient);
    }
}