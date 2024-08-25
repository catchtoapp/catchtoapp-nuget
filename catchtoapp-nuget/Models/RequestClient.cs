using System.ComponentModel.DataAnnotations;

namespace catchtoapp_nuget.Models
{
    public class RequestClient<TBody>
    {
        [Required]
        public string? Url { get; set; }
        [Required]
        public HttpMethod? HttpMethod { get; set; }
        public ClientAuthentication? ClientAuthentication { get; set; }
        public Dictionary<string, string>? UriParameters { get; set; }
        public TBody? BodyContent { get; set; }
    }
}
