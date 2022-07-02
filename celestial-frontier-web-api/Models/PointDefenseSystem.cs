using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class PointDefenseSystem
    {
        public PointDefenseSystem()
        {
            FleetBattlecruisers = new HashSet<FleetBattlecruiser>();
            FleetCarriers = new HashSet<FleetCarrier>();
            FleetCorvettes = new HashSet<FleetCorvette>();
            FleetDestroyers = new HashSet<FleetDestroyer>();
            FleetFighters = new HashSet<FleetFighter>();
            FleetFrigates = new HashSet<FleetFrigate>();
        }

        public Guid PointDefenseSystemGuid { get; set; }
        public Guid PowerLevelGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int CountermeasureCapacity { get; set; }
        public int CountermeasureRegenerationRate { get; set; }
        public double Efficiency { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual PowerLevel PowerLevel { get; set; }
        public virtual ICollection<FleetBattlecruiser> FleetBattlecruisers { get; set; }
        public virtual ICollection<FleetCarrier> FleetCarriers { get; set; }
        public virtual ICollection<FleetCorvette> FleetCorvettes { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyers { get; set; }
        public virtual ICollection<FleetFighter> FleetFighters { get; set; }
        public virtual ICollection<FleetFrigate> FleetFrigates { get; set; }
    }
}
