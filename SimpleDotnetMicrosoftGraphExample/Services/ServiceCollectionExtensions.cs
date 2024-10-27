using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using SimpleOneDriveSdkSample.Services.MsGraph;

namespace SimpleOneDriveSdkSample.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMicrosoftGraphService(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services
            .Configure<MicrosoftAuthOptions>(configuration.GetSection(MicrosoftAuthOptions.SectionName))
            .AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>()
            .AddSingleton<GraphServiceClient>(provider =>
            {
                var microsoftGraph = provider.GetRequiredService<IMicrosoftGraphService>();
                var interactiveCredential = microsoftGraph.CreateTokenCredential().GetAwaiter().GetResult();
                return new GraphServiceClient(interactiveCredential, microsoftGraph.Scopes);
            });
        return services;
    }
}