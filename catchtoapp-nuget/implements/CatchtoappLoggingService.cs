using catchtoapp_nuget.contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catchtoapp_nuget.implements
{
    public class CatchtoappLoggingService : ICatchtoappLoggingService
    {
        private readonly ILogger<CatchtoappLoggingService> _logger;

        public CatchtoappLoggingService(ILogger<CatchtoappLoggingService> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
