# Simple .NET Microsoft Graph SDK Example

Provides a simple .NET 8 based console app implementation of the Microsoft Graph SDK which shows authentication and retrieval of a user's OneDrive root drive.

## About the code

This sample originated while trying to learn how to create an application that uses the Microsoft Graph to access OneDrive (see [Getting started with OneDrive API](https://learn.microsoft.com/en-us/onedrive/developer/rest-api/getting-started/?view=odsp-graph-online))

After following the [quickstart tutorial](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-desktop-app-wpf-sign-in#option-2-register-and-manually-configure-your-application-and-code-sample) I had a functional prototype that authenticated to the graph using the MSAL library, but the implementation felt heavy and did not illustrate how to encapsulate the login functionality into the .NET dependency injection system.

More research turned up the following two pages which this example utilized to implment authentication into a dependency injected service along with the GraphServiceClient so all that is necessary to make a class capable of calling the Micrsoft Graph is to inject the `GraphServiceClient` class into the constructor.

- [Token caching in the Azure Identity client library](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/samples/TokenCache.md) - explained how to authenticate

- [Token caching in the Azure Identity client library](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/samples/TokenCache.md) - Made it possible to serialize the token credential to a disk file so you can avoid having to login when the app is loaded later. The code automatically handles displaying the Microsoft authentication Web UI whenever login is required.

## Running the sample

### Step 1: Register your application

To register your application and add the app's registration information to your solution manually, follow these steps:

1. Sign in to the [Microsoft Entra admin center](https://entra.microsoft.com/).

1. If you have access to multiple tenants, use the Settings icon ![settings icon](./assets/gear_icon.png) in the top menu to switch to the tenant in which you want to register the application from the **Directories** + **subscriptions** menu.

1. Browse to **Identity** > **Applications** > **App registrations**, select **New** registration.

1. Enter a **Name** for your application, for example *My Application*. Users of your app might see this name, and you can change it later.

1. In the **Supported Account Types** section, select *Accounts in any identity provider or organizational directory (for authenticating users with user flows)*.

1. In the **Redirect URI (recommended)** section, select *Public client/native (mobile & desktop)* and enter `http://localhost` as the redirect uri.

1. Under the **Permissions** section, keep the *Grant admin consent to openid and offline_access permissions* checkbox checked.

1. Click the **Register** button to create the application.


### Step 2: Update the appsettings.json file with your ApplicationId/ClientId

1. In the appsettings.json file, set the `ClientId` property to the value shown in the *Application (client) ID* field under the **Overview** tab in the app registration.

2. Run the project.
    - It should prompt you to login.
    - Login should complete successfully.
    - A file named `token.cache` will be saved in the application bin directory which will be used on subsequent application loads.
    - Subsequent loads should not require login until the current login expires. At that point the browser should show the login screen to allow you to login. this is all handled inside the Azure.Identity pacakage in conjunction with the web browser.
    - You can delete the token.cache file to require login again.

## Notes

- The current implementation does not include *Sign out* logic.
- The current implmementation will not recognize scopes being changed in the configuration file which would require login to be re-done.
- You can delete the token cache file to get around this for now.