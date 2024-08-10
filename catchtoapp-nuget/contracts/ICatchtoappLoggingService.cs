using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catchtoapp_nuget.contracts
{
    public interface ICatchtoappLoggingService
    {
        void LogInformation(string message);
    }
}
