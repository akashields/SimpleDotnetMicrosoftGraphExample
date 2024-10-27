using Azure.Core;

namespace SimpleOneDriveSdkSample.Services;

public interface IMicrosoftGraphService
{
    Task<TokenCredential> CreateTokenCredential();
    string[] Scopes { get; }
}