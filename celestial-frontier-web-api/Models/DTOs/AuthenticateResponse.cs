namespace celestial_frontier_web_api.Models.DTOs {
    public class AuthenticateResponse {
        public Guid UserGuid { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserDto userDto, string token) {
            this.UserGuid = userDto.UserGuid;
            this.Username = userDto.Username;
            Token = token;
        }
    }
}
