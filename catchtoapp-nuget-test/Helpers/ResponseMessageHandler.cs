namespace catchtoapp_nuget_test.Helpers
{
    public class ResponseMessageHandler : HttpMessageHandler
    {
        private readonly Queue<HttpResponseMessage> _response = new Queue<HttpResponseMessage>();

        internal void SetResponse(HttpResponseMessage response)
        {
            _response.Enqueue(response);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response.Dequeue());
        }
    }
}
