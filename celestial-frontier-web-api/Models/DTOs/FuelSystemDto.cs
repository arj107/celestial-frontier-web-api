namespace celestial_frontier_web_api.Models.DTOs {
    public class FuelSystemDto {
        public Guid FuelSystemGuid { get; set; }
        public SpaceshipTypeDto SpaceshipType { get; set; } = null!;
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public double FuelCapacity { get; set; }
        public double FuelRegenerationRate { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public FuelSystemDto(FuelSystem fuelSystem) {
            this.FuelSystemGuid = fuelSystem.FuelSystemGuid;
            this.SpaceshipType = new SpaceshipTypeDto(fuelSystem.SpaceshipType);
            this.ModulePointsCost = fuelSystem.ModulePointsCost;
            this.Mass = fuelSystem.Mass;
            this.SpecificHeatCapacity = fuelSystem.SpecificHeatCapacity;
            this.FuelCapacity = fuelSystem.FuelCapacity;
            this.FuelRegenerationRate = fuelSystem.FuelRegenerationRate;
            this.Title = fuelSystem.Title;
            this.Description = fuelSystem.Description;
        }
    }
}
