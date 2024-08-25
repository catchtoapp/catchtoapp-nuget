using System.ComponentModel.DataAnnotations;

namespace catchtoapp_nuget.Models
{
    public class ClientAuthentication
    {
        [Required]
        public string? ClientAppId { get; set; }
        [Required]
        public string? ClientSecret { get; set; }
        [Required]
        public string? Resource { get; set; }
        [Required]
        public string? Authority { get; set; }
        public string? OcpKey { get; set; }
        public string? OcpTrace { get; set; }
    }
}
