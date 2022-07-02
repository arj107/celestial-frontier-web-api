namespace celestial_frontier_web_api.Models.DTOs {
    public class PowerLevelDto {
        public PowerLevelDto() {}
        public PowerLevelDto(PowerLevel powerLevel) {
            this.PowerLevelGuid = powerLevel.PowerLevelGuid;
            this.Text = powerLevel.Text;
        }

        public Guid PowerLevelGuid {
            get; set;
        }
        public string Text { get; set; } = null!;
    }
}
