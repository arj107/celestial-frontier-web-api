using System.ComponentModel.DataAnnotations;

namespace celestial_frontier_web_api.Models.DTOs {
    public class AuthenticateRequest {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
