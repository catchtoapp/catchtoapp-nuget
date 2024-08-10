using catchtoapp_nuget.implements;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

namespace catchtoapp_nuget_test
{
    public class CatchtoappLoggingServiceTest
    {

        private readonly ITestOutputHelper _outputHelper;

        public CatchtoappLoggingServiceTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void MessageCatchtoappLoggingServiceTest()
        {
            _outputHelper.WriteLine("MessageCatchtoappLoggingServiceTest");
        }


        [Fact]
        public void LogInformationCatchtoappLoggingServiceTest()
        {
            var mockLogger = new Mock<ILogger<CatchtoappLoggingService>>();
            var objCatchtoappLoggingService = new CatchtoappLoggingService(mockLogger.Object);
            objCatchtoappLoggingService.LogInformation("Mensaje");
            mockLogger.VerifyAll();
        }
    }
}