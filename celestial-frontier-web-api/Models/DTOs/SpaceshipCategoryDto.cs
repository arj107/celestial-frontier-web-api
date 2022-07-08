namespace celestial_frontier_web_api.Models.DTOs {
    public class SpaceshipCategoryDto {
        public SpaceshipCategoryDto() {}
        public SpaceshipCategoryDto(SpaceshipCategory spaceshipCategory) {
            this.SpaceshipCategoryGuid = spaceshipCategory.SpaceshipCategoryGuid;
            this.Text = spaceshipCategory.Text;
        }

        public Guid SpaceshipCategoryGuid {
            get; set;
        }
        public string Text { get; set; } = null!;

    }
}
