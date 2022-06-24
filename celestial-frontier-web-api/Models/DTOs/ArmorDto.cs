namespace celestial_frontier_web_api.Models.DTOs {
    public class ArmorDto {
        public Guid ArmorGuid { get; set; }
        public SpaceshipTypeDto SpaceshipType { get; set; } = null!;
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public double KineticResistance { get; set; }
        public double EnergyResistance { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ArmorDto(Armor armor) {
            this.ArmorGuid = armor.ArmorGuid;
            this.SpaceshipType = new SpaceshipTypeDto(armor.SpaceshipType);
            this.ModulePointsCost = armor.ModulePointsCost;
            this.Mass = armor.Mass;
            this.SpecificHeatCapacity = armor.SpecificHeatCapacity;
            this.KineticResistance = armor.KineticResistance;
            this.EnergyResistance = armor.EnergyResistance;
            this.Title = armor.Title;
            this.Description = armor.Description;
        }
    }
}
