namespace celestial_frontier_web_api.Models.DTOs {
    public class ThrusterDto {
        public Guid ThrusterGuid { get; set; }
        public SpaceshipTypeDto SpaceshipType { get; set; } = null!;
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public double Thrust { get; set; }
        public double Efficiency { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ThrusterDto(Thruster thruster) {
            this.ThrusterGuid = thruster.ThrusterGuid;
            this.SpaceshipType = new SpaceshipTypeDto(thruster.SpaceshipType);
            this.ModulePointsCost = thruster.ModulePointsCost;
            this.Mass = thruster.Mass;
            this.SpecificHeatCapacity = thruster.SpecificHeatCapacity;
            this.Thrust = thruster.Thrust;
            this.Efficiency = thruster.Efficiency;
            this.Title = thruster.Title;
            this.Description = thruster.Description;
        }
    }
}
