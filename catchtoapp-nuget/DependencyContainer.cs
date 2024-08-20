using catchtoapp_nuget.contracts;
using catchtoapp_nuget.implements;
using catchtoapp_nuget_blobstorage;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace catchtoapp_nuget
{
    [ExcludeFromCodeCoverage(Justification = "Class static for container dependency")]
    public static class DependencyContainer
    {
        public static void AddCatchtoappContainerLogger(this IServiceCollection service)
        {
            service.AddLogging();
            service.AddApplicationInsightsTelemetry();
            service.AddCatchtoappContainerBlobStorage();
            service.AddTransient<ICatchtoappLoggingService, CatchtoappLoggingService>();
        }
    }
}
