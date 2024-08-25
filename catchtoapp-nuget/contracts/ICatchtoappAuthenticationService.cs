using catchtoapp_nuget.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace catchtoapp_nuget.contracts
{
    public interface ICatchtoappAuthenticationService
    {
        Task<AuthenticationResult> AcquireTokenAsync(ClientAuthentication clientAuthentication);
    }
}
