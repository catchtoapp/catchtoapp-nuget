using Azure.Identity;
using Azure.Core;
using catchtoapp_nuget.contracts;
using catchtoapp_nuget.implements;
using catchtoapp_nuget_blobstorage.implements;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace catchtoapp_nuget
{
    [ExcludeFromCodeCoverage(Justification = "Class static for container dependency")]
    public static class DependencyContainer
    {
        public static void AddCatchtoappContainerLogger(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddLogging();
            services.AddApplicationInsightsTelemetry();

            string? blobPath = Environment.GetEnvironmentVariable("UriStorageAccount");
            if (!string.IsNullOrEmpty(blobPath))
            {
                services.AddAzureClients(c => c.AddBlobServiceClient(new Uri(blobPath))
                .WithCredential(new DefaultAzureCredential())
                .ConfigureOptions(o =>
                {
                    o.Retry.MaxRetries = 3;
                    o.Retry.Mode = RetryMode.Exponential;
                    o.Retry.Delay = TimeSpan.FromSeconds(3);
                    o.Retry.NetworkTimeout = TimeSpan.FromSeconds(120);
                    o.Retry.MaxDelay = TimeSpan.FromSeconds(20);
                }));
            }

            services.AddTransient<ICatchtoappLoggingService, CatchtoappLoggingService>();
            services.AddTransient<ICatchtoappBlobStorageService, CatchtoappBlobStorageService>();
            services.AddTransient<ICatchtoappAuthenticationService, CatchtoappAuthenticationService>();
            services.AddTransient<ICatchtoappRestClientService, CatchtoappRestClientService>();
        }
    }
}
