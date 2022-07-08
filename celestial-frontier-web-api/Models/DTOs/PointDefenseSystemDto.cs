namespace celestial_frontier_web_api.Models.DTOs {
    public class PointDefenseSystemDto {
        public Guid PointDefenseSystemGuid { get; set; }
        public PowerLevelDto PowerLevel { get; set; } = null!;
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int CountermeasureCapacity { get; set; }
        public int CountermeasureRegenerationRate { get; set; }
        public double Efficiency { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public PointDefenseSystemDto() {}
        public PointDefenseSystemDto(PointDefenseSystem pointDefenseSystem) {
            this.PointDefenseSystemGuid = pointDefenseSystem.PointDefenseSystemGuid;
            this.PowerLevel = new PowerLevelDto(pointDefenseSystem.PowerLevel);
            this.ModulePointsCost = pointDefenseSystem.ModulePointsCost;
            this.Mass = pointDefenseSystem.Mass;
            this.SpecificHeatCapacity = pointDefenseSystem.SpecificHeatCapacity;
            this.CountermeasureCapacity = pointDefenseSystem.CountermeasureCapacity;
            this.CountermeasureRegenerationRate = pointDefenseSystem.CountermeasureRegenerationRate;
            this.Efficiency = pointDefenseSystem.Efficiency;
            this.Title = pointDefenseSystem.Title;
            this.Description = pointDefenseSystem.Description;
        }
    }
}
