using catchtoapp_nuget.contracts;
using catchtoapp_nuget.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace catchtoapp_nuget.implements
{
    public class CatchtoappAuthenticationService : ICatchtoappAuthenticationService
    {
        public async Task<AuthenticationResult> AcquireTokenAsync(ClientAuthentication clientAuthentication)
        {
            AuthenticationContext authContext = new AuthenticationContext(clientAuthentication.Authority);
            return await authContext.AcquireTokenAsync(clientAuthentication.Resource, new ClientCredential(clientAuthentication.ClientAppId, clientAuthentication.ClientSecret));
        }
    }
}
