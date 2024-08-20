using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Azure.Core;
using Azure.Identity;
using catchtoapp_nuget.contracts;
using catchtoapp_nuget_blobstorage.implements;

namespace catchtoapp_nuget_blobstorage
{
    [ExcludeFromCodeCoverage(Justification = "Class static for container dependency")]
    public static class DependencyContainer
    {
        public static void AddCatchtoappContainerBlobStorage(this IServiceCollection service)
        {
            string? blobPath = Environment.GetEnvironmentVariable("UriStorageAccount");
            if (!string.IsNullOrEmpty(blobPath)) { 
                service.AddAzureClients(c => c.AddBlobServiceClient(blobPath)
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
            service.AddTransient<ICatchtoappBlobStorageService, CatchtoappBlobStorageService>();
        }
    }
}
