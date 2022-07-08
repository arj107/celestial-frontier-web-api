namespace celestial_frontier_web_api.Models.DTOs {
    public class SpaceshipTypeDto {
        public SpaceshipTypeDto() {}
        public SpaceshipTypeDto(SpaceshipType spaceshipType) {
            this.SpaceshipTypeGuid = spaceshipType.SpaceshipTypeGuid;
            this.Text = spaceshipType.Text;
        }

        public Guid SpaceshipTypeGuid { get; set; }
        public string Text { get; set; } = null!;
    }
}
