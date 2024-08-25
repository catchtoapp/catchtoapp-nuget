using catchtoapp_nuget.Models;

namespace catchtoapp_nuget.contracts
{
    public interface ICatchtoappRestClientService
    {
        Task<TResult> Send<TBody, TResult>(RequestClient<TBody> requestClient);
        Task<HttpResponseMessage> Send<TBody>(RequestClient<TBody> requestClient);
    }
}
