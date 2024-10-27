using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using SimpleOneDriveSdkSample.Services;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(prefix: "MS_GRAPH_SAMPLE_")
    .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
    .AddCommandLine(Environment.GetCommandLineArgs())
    .Build();

await new ServiceCollection()
    .AddMicrosoftGraphService(configuration)
    .AddSingleton(configuration)
    .AddTransient<App>()
    .BuildServiceProvider()
    .GetRequiredService<App>().Run();

internal class App(IConfigurationRoot configuration, GraphServiceClient graphClient)
{
    public async Task Run()
    {
        var drive  = await graphClient.Me.Drive.GetAsync();
        Console.WriteLine("Drive ID: " + drive?.Owner?.User?.DisplayName ?? "Unknown User");
    }
}