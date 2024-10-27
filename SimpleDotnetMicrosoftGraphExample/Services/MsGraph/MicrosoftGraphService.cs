using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Options;

namespace SimpleOneDriveSdkSample.Services.MsGraph;

public class MicrosoftGraphService(IOptions<MicrosoftAuthOptions> options) : IMicrosoftGraphService
{
     public async Task<TokenCredential> CreateTokenCredential()
    {
        // https://learn.microsoft.com/dotnet/api/azure.identity.interactivebrowsercredential
        // https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/samples/TokenCache.md
        
        InteractiveBrowserCredential? credential;
        
        var graphOptions = options.Value;
        var credentialOptions = new InteractiveBrowserCredentialOptions
        {
            TenantId = graphOptions.TenantId,
            ClientId = graphOptions.ClientId,
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            // MUST be http://localhost or http://localhost:PORT
            // See https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core
            RedirectUri = new Uri(graphOptions.RedirectUri),
            TokenCachePersistenceOptions = new TokenCachePersistenceOptions()
        };
        
        if (!File.Exists(options.Value.TokenCacheFileName))
        {
            credential = new InteractiveBrowserCredential(credentialOptions);
            
            AuthenticationRecord authRecord = await credential.AuthenticateAsync(new TokenRequestContext(graphOptions.ScopesArray));

            await using var authRecordStream = new FileStream(graphOptions.TokenCacheFileName, FileMode.Create, FileAccess.Write);
            await authRecord.SerializeAsync(authRecordStream);
        }
        else
        {
            await using var authRecordStream = new FileStream(graphOptions.TokenCacheFileName, FileMode.Open, FileAccess.Read);
            var authRecord = await AuthenticationRecord.DeserializeAsync(authRecordStream);

            credentialOptions.AuthenticationRecord = authRecord;
            credential = new InteractiveBrowserCredential(credentialOptions);
        }

        return credential;
    }

    public string[] Scopes => options.Value.ScopesArray;
}