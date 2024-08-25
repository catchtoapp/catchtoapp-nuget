using Azure.Core;
using catchtoapp_nuget.contracts;
using catchtoapp_nuget.implements;
using catchtoapp_nuget.Models;
using catchtoapp_nuget_test.Helpers;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace catchtoapp_nuget_test
{
    public class CatchtoappRestClientServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CatchtoappRestClientServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void MessageCatchtoappRestClientServiceTest()
        {
            _testOutputHelper.WriteLine("MessageCatchtoappRestClientServiceTest");
        }

        [Fact]
        public async void SendHttpResponseMessageCorrect ()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.OK);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);


            // Act
            Dictionary<string, string> valorParam = new Dictionary<string, string>
            {
                {"value","1"}
            };


            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            var request = new RequestClient<Datos>
            {

                HttpMethod = HttpMethod.Get,
                ClientAuthentication = authentication,
                Url = "https://localhost.com/",
                UriParameters = valorParam
            };

            var response = await _apiClient.Send<Datos>(request);
            
            // Assert
            Assert.NotNull(response);
        }

        [Fact]

        public async void SendHttpResponseMessageCorrectJson()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.OK);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);

            // Act
            Datos obj = new Datos()
            {
                valor = "1234"
            };

            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            var request = new RequestClient<Datos>
            {
                HttpMethod = HttpMethod.Post,
                ClientAuthentication = authentication,
                Url = "https://localhost.com/",
                BodyContent = obj
            };

            var response = await _apiClient.Send<Datos>(request);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void SendHttpResponseMessageNullParameterUrlEncode()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.OK);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);

            // Act
            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            var request = new RequestClient<Datos>
            {
                HttpMethod = HttpMethod.Get,
                ClientAuthentication = authentication,
                Url = "https://localhost.com/",
                UriParameters = null
            };

            var response = await _apiClient.Send<Datos>(request);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void SendHttpResponseMessage0ParameterUrlEncode()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.OK);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);

            // Act
            Dictionary<string, string> valorParam = new Dictionary<string, string>();

            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            var request = new RequestClient<Datos>
            {
                HttpMethod = HttpMethod.Get,
                ClientAuthentication = authentication,
                Url = "https://localhost.com/",
                UriParameters = valorParam
            };

            var response = await _apiClient.Send<Datos>(request);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void SendHttpResponseMessageModelInvalidJson()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.OK);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);

            // Act
            Datos obj = new Datos()
            {
                valor = "1234"
            };

            var request = new RequestClient<Datos>
            {
                Url = "https://localhost.com/",
                BodyContent = obj
            };

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _apiClient.Send<Datos>(request));
        }

        [Fact]
        public async void SendTResultBadUrlEncode()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.BadRequest);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);

            // Act
            Dictionary<string, string> valorParam = new Dictionary<string, string>
            {
                {"valor","1"},
            };

            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            var request = new RequestClient<Datos>
            {
                HttpMethod = HttpMethod.Get,
                ClientAuthentication = authentication,
                Url = "https://localhost.com/",
                UriParameters = valorParam
            };

            // Assert
            await Assert.ThrowsAsync<Exception>(() => _apiClient.Send<Datos, ValorPrueba>(request));
        }

        [Fact]
        public async void SendTResultCorrectUrlEncode()
        {
            // Arrange
            HttpClient _httpClient = ResponseClient.ResponseHttpClient(HttpStatusCode.OK);
            var mockAuthService = new Mock<ICatchtoappAuthenticationService>();
            mockAuthService.Setup(x => x.AcquireTokenAsync(It.IsAny<ClientAuthentication>())).ReturnsAsync(ResponseClient.GetAuthResult());
            CatchtoappRestClientService _apiClient = new CatchtoappRestClientService(_httpClient, mockAuthService.Object);

            // Act
            Dictionary<string, string> valorParam = new Dictionary<string, string>
            {
                {"valor","1"},
            };

            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            var request = new RequestClient<Datos>
            {
                HttpMethod = HttpMethod.Get,
                ClientAuthentication = authentication,
                Url = "https://localhost.com/",
                UriParameters = valorParam
            };

            var response = await _apiClient.Send<Datos, ValorPrueba>(request);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async void SendAuthenticationFail()
        {
            // Arrange
            ClientAuthentication authentication = new ClientAuthentication
            {
                Authority = "https://localhost.com/",
                Resource = "Resource",
                ClientAppId = "ClientAppId",
                ClientSecret = "ClientSecret",
                OcpKey = "OcpKey",
                OcpTrace = "true"
            };

            CatchtoappAuthenticationService catchtoappAuthenticationService = new CatchtoappAuthenticationService();
            var response = catchtoappAuthenticationService.AcquireTokenAsync(authentication);
            Assert.NotNull(response);
        }
    }
}
