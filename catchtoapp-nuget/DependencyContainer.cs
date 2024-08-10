using catchtoapp_nuget.contracts;
using catchtoapp_nuget.implements;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace catchtoapp_nuget
{
    [ExcludeFromCodeCoverage(Justification = "Class static for container dependency")]
    public static class DependencyContainer
    {
        public static IServiceCollection AddCatchtoappContainer(this IServiceCollection service)
        {
            service.AddTransient<ICatchtoappLoggingService, CatchtoappLoggingService>();
            service.AddLogging();
            return service;
        }
    }
}
