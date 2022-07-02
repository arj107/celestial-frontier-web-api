using Newtonsoft.Json;

namespace celestial_frontier_web_api.Models.DTOs {
    public class UserDto {
        public Guid UserGuid { get; set; }
        public string Username { get; set; }

        public UserDto() {}
        public UserDto(User user) {
            this.UserGuid = user.UserGuid;
            this.Username = user.Username;
        }
    }
}
