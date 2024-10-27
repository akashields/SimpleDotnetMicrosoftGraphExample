namespace SimpleOneDriveSdkSample.Services.MsGraph;

public class MicrosoftAuthOptions
{
    public const string SectionName = "Authentication:Microsoft";
    
    public string Instance { get; set; } =  "https://login.microsoftonline.com/";
    public string TenantId { get; set; } = "common";
    public string? ClientId { get; set; }
    public string RedirectUri { get; set; } = "http://localhost";
    public string Scopes { get; set; } = "User.Read Files.Read";
    public string TokenCacheFileName { get; set; } = "token.cache";
    public string MicrosoftGraphApiBaseUrl { get; set; } = "https://graph.microsoft.com";
    
    public string[] ScopesArray => Scopes.Split(' ');
}