using System.ComponentModel.DataAnnotations;

namespace catchtoapp_nuget_models
{
    public class RequestModel
    {
        public string? HttpMethod { get; set; }
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string RequestResponse { get; set; }
        [Required]
        public string UriStorageAccount { get; set; }
        [Required]
        public string Container { get; set; }
        [Required]
        public string FileName { get; set; }
    }
}
