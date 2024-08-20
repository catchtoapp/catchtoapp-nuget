using catchtoapp_nuget.contracts;
using catchtoapp_nuget.implements;
using catchtoapp_nuget_models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

namespace catchtoapp_nuget_test
{
    public class CatchtoappLoggingServiceTest
    {

        private readonly ITestOutputHelper _outputHelper;
        private readonly TelemetryClient _mockTelemetry;

        public CatchtoappLoggingServiceTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            var config = new TelemetryConfiguration
            {
                TelemetryChannel = new Mock<ITelemetryChannel>().Object
            };
            _mockTelemetry = new TelemetryClient(config);
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
            var mockBlobStorage = new Mock<ICatchtoappBlobStorageService>();
            var objCatchtoappLoggingService = new CatchtoappLoggingService(mockLogger.Object, _mockTelemetry, mockBlobStorage.Object);
            objCatchtoappLoggingService.LogInformation("LogInformationCatchtoappLoggingServiceTest: Mensaje de prueba");
            mockLogger.VerifyAll();
        }

        [Fact]
        public void LogErrorCatchtoappLoggingServiceTest()
        {
            var mockLogger = new Mock<ILogger<CatchtoappLoggingService>>();
            var mockBlobStorage = new Mock<ICatchtoappBlobStorageService>();
            var objCatchtoappLoggingService = new CatchtoappLoggingService(mockLogger.Object, _mockTelemetry, mockBlobStorage.Object);
            objCatchtoappLoggingService.LogError("LogErrorCatchtoappLoggingServiceTest: Mensaje de prueba",new Exception("Error generado"));
            mockLogger.VerifyAll();
        }

        [Fact]
        public void LogCustomEventCatchtoappLoggingServiceTest()
        {
            var mockLogger = new Mock<ILogger<CatchtoappLoggingService>>();
            var mockBlobStorage = new Mock<ICatchtoappBlobStorageService>();
            var objCatchtoappLoggingService = new CatchtoappLoggingService(mockLogger.Object, _mockTelemetry, mockBlobStorage.Object);

            var dictionaryTest = new Dictionary<string, string>
            {
                {"1", "valor 1"},
                {"2", "valor 2"}
            };

            objCatchtoappLoggingService.LogCustomEvent("LogCustomEventCatchtoappLoggingServiceTest: Mensaje de prueba", dictionaryTest);
            mockLogger.VerifyAll();
        }

        [Fact]
        public async void LogRequestEmptyCatchtoappLoggingServiceTest()
        {
            var mockLogger = new Mock<ILogger<CatchtoappLoggingService>>();
            var mockBlobStorage = new Mock<ICatchtoappBlobStorageService>();
            var objCatchtoappLoggingService = new CatchtoappLoggingService(mockLogger.Object, _mockTelemetry, mockBlobStorage.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net",
                Container = "container",
                FileName = "filename",
                RequestBody = "requestbody",
                RequestResponse = "requestResponse",
                HttpMethod = "post",
                Url = "https://localhost.com"
            };

            await objCatchtoappLoggingService.LogRequest(request);
            mockLogger.VerifyAll();
        }

        [Fact]
        public async void LogRequestNotEmptyCatchtoappLoggingServiceTest()
        {
            var mockLogger = new Mock<ILogger<CatchtoappLoggingService>>();
            var mockBlobStorage = new Mock<ICatchtoappBlobStorageService>();
            var objCatchtoappLoggingService = new CatchtoappLoggingService(mockLogger.Object, _mockTelemetry, mockBlobStorage.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net",
                Container = "",
                FileName = "filename",
                RequestBody = "requestbody",
                RequestResponse = "requestResponse",
                HttpMethod = "post",
                Url = "https://localhost.com"
            };

            mockBlobStorage.Setup(x => x.AddFileLogger(request)).Returns(Task.FromResult("Model invalid."));

            await objCatchtoappLoggingService.LogRequest(request);
            mockLogger.VerifyAll();
        }
    }
}