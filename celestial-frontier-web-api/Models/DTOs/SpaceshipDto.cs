namespace celestial_frontier_web_api.Models.DTOs {
    public class SpaceshipDto {
        public Guid SpaceshipGuid { get; set; }
        public SpaceshipTypeDto SpaceshipType { get; set; } = null!;
        public SpaceshipCategoryDto SpaceshipCategory { get; set; } = null!;
        public PowerLevelDto WeaponPowerLevel { get; set; } = null!;
        public PowerLevelDto PointDefenseSystemPowerLevel { get; set; } = null!;
        public string Class { get; set; } = null!;
        public int HullMass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseHullHealth { get; set; }
        public string Description { get; set; } = null!;

        public SpaceshipDto(Spaceship spaceship) {
            this.SpaceshipGuid = spaceship.SpaceshipGuid;
            this.SpaceshipType = new SpaceshipTypeDto(spaceship.SpaceshipType);
            this.SpaceshipCategory = new SpaceshipCategoryDto(spaceship.SpaceshipCategory);
            this.WeaponPowerLevel = new PowerLevelDto(spaceship.WeaponPowerLevel);
            this.PointDefenseSystemPowerLevel = new PowerLevelDto(spaceship.PointDefenseSystemPowerLevel);
            this.Class = spaceship.Class;
            this.HullMass = spaceship.HullMass;
            this.SpecificHeatCapacity = spaceship.SpecificHeatCapacity;
            this.BaseHullHealth = spaceship.BaseHullHealth;
            this.Description = spaceship.Description;
        }
    }
}
