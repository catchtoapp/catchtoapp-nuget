using catchtoapp_nuget.contracts;
using catchtoapp_nuget_models;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace catchtoapp_nuget.implements
{
    public class CatchtoappLoggingService : ICatchtoappLoggingService
    {
        private readonly ILogger<CatchtoappLoggingService> _logger;
        private readonly TelemetryClient _telemetryClient;
        private readonly ICatchtoappBlobStorageService _catchtoappBlobStorageService;

        public CatchtoappLoggingService(ILogger<CatchtoappLoggingService> logger, TelemetryClient telemetryClient, ICatchtoappBlobStorageService catchtoappBlobStorageService)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
            _catchtoappBlobStorageService = catchtoappBlobStorageService;
        }

        public void LogCustomEvent(string eventName, IDictionary<string, string>? properties = null)
        {
            _telemetryClient.TrackEvent(eventName, properties);
        }

        public void LogError(string message, Exception ex, params object?[] args)
        {
            _logger.Log(LogLevel.Error, message, ex, args);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public async Task LogRequest(RequestModel request)
        {
            var result = await _catchtoappBlobStorageService.AddFileLogger(request);
            if (!string.IsNullOrEmpty(result)) { 
                _logger.LogError(result);
            }
        }
    }
}
