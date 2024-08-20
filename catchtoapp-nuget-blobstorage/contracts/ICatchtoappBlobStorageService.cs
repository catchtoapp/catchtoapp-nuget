using catchtoapp_nuget_models;

namespace catchtoapp_nuget.contracts
{
    public interface ICatchtoappBlobStorageService
    {
        Task<string> AddFileLogger(RequestModel request);
    }
}
