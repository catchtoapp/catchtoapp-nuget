using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net;
using System.Reflection;

namespace catchtoapp_nuget_test.Helpers
{
    public static class ResponseClient
    {
        public static HttpClient ResponseHttpClient(HttpStatusCode statusCode)
        {
            ResponseMessageHandler handler = new ResponseMessageHandler();
            HttpResponseMessage response = new HttpResponseMessage(statusCode);
            handler.SetResponse(response);
            return new HttpClient(handler);
        }

        public static AuthenticationResult GetAuthResult()
        {
            string accessTokenType = "Bearer";
            string accessToken = "abcd";
            DateTimeOffset expiresOn = DateTimeOffset.Now.AddHours(1);
            var typeOfAdalResult = Type.GetType("Microsoft.Identity.Core.Cache.AdalResult, Microsoft.IdentityModel.Clients.ActiveDirectory");
            var ctorOfAdalResult = typeOfAdalResult.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(string), typeof(string), typeof(DateTimeOffset) }, null);
            object adalResultInstance = ctorOfAdalResult.Invoke(new object[] { accessTokenType, accessToken, expiresOn });
            var ctorAuthenticationResult = typeof(AuthenticationResult).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeOfAdalResult }, null);
            AuthenticationResult athenticationResultInstance = (AuthenticationResult)ctorAuthenticationResult.Invoke(new object[] { adalResultInstance });
            return athenticationResultInstance;
        }
    }
}
