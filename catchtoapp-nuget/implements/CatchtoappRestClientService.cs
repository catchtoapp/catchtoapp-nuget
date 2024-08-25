using catchtoapp_nuget.contracts;
using catchtoapp_nuget.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace catchtoapp_nuget.implements
{
    public class CatchtoappRestClientService : ICatchtoappRestClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ICatchtoappAuthenticationService _authenticationService;
        private readonly JsonSerializerSettings _jsonSettings;

        public CatchtoappRestClientService(HttpClient httpClient, ICatchtoappAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _authenticationService = authenticationService;
            _jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        public async Task<TResult> Send<TBody, TResult>(RequestClient<TBody> requestClient)
        {
            try
            {
                HttpResponseMessage response = await Send(requestClient);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(stream);
                string responseBody = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<TResult>(responseBody, _jsonSettings);
            }
            catch (Exception ex)
            {
                throw new Exception("Trace: " + ex.StackTrace + " " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> Send<TBody>(RequestClient<TBody> requestClient)
        {
            try
            {
                var vcRequestModel = new ValidationContext(requestClient);
                ICollection<ValidationResult> vcResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(requestClient, vcRequestModel, vcResults, true))
                {
                    var errors = string.Empty;
                    foreach (var validationResult in vcResults)
                        errors = string.Join(",", errors);
                    throw new Exception(errors);
                }

                HttpRequestMessage httpMessage;
                string queryString = ToQueryString(requestClient.UriParameters);
                if (string.IsNullOrEmpty(queryString))
                {
                    var json = JsonConvert.SerializeObject(requestClient.BodyContent, _jsonSettings);
                    httpMessage = new HttpRequestMessage(requestClient.HttpMethod, new Uri(requestClient.Url))
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                }
                else
                {
                    httpMessage = new HttpRequestMessage(requestClient.HttpMethod, new Uri($"{requestClient.Url}?{queryString}"));
                }

                await SetAuthorization(requestClient.ClientAuthentication);
                return await _httpClient.SendAsync(httpMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Trace: " + ex.StackTrace + " " + ex.Message);
            }
        }

        private async Task SetAuthorization(ClientAuthentication auth)
        {
            AuthenticationResult token = await _authenticationService.AcquireTokenAsync(auth);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", auth.OcpKey);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Trace", auth.OcpTrace);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"{token.AccessTokenType} {token.AccessToken}");
        }

        private string ToQueryString(Dictionary<string, string> urlParams)
        {
            var parameters = string.Empty;
            if (urlParams != null && urlParams.Any())
                parameters = string.Join("&", urlParams.Where(x => !string.IsNullOrEmpty(x.Key)).Select(y => string.Format("{0}={1}", y.Key, y.Value)));
            return parameters;
        }
    }
}
