using catchtoapp_nuget_models;

namespace catchtoapp_nuget.contracts
{
    public interface ICatchtoappLoggingService
    {
        void LogInformation(string message);
        void LogError(string message, Exception ex, params object?[] args);
        void LogCustomEvent(string eventName, IDictionary<string,string>? properties = null);
        Task LogRequest(RequestModel request);
    }
}
