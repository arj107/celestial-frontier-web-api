using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FuelSystem
    {
        public FuelSystem()
        {
            BattlecruiserConfigurations = new HashSet<BattlecruiserConfiguration>();
            CarrierConfigurations = new HashSet<CarrierConfiguration>();
            CorvetteConfigurations = new HashSet<CorvetteConfiguration>();
            DestroyerConfigurations = new HashSet<DestroyerConfiguration>();
            FighterConfigurations = new HashSet<FighterConfiguration>();
            FrigateConfigurations = new HashSet<FrigateConfiguration>();
        }

        public Guid FuelSystemGuid { get; set; }
        public Guid SpaceshipTypeGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public double FuelCapacity { get; set; }
        public double FuelRegenerationRate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual SpaceshipType SpaceshipType { get; set; }
        public virtual ICollection<BattlecruiserConfiguration> BattlecruiserConfigurations { get; set; }
        public virtual ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }
        public virtual ICollection<CorvetteConfiguration> CorvetteConfigurations { get; set; }
        public virtual ICollection<DestroyerConfiguration> DestroyerConfigurations { get; set; }
        public virtual ICollection<FighterConfiguration> FighterConfigurations { get; set; }
        public virtual ICollection<FrigateConfiguration> FrigateConfigurations { get; set; }
    }
}
