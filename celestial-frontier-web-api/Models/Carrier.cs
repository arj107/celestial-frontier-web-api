using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Carrier
    {
        public Carrier()
        {
            FleetCarriers = new HashSet<FleetCarrier>();
        }

        public Guid CarrierGuid { get; set; }
        public Guid SpaceshipTypeGuid { get; set; }
        public Guid SpaceshipCategoryGuid { get; set; }
        public Guid WeaponPowerLevelGuid { get; set; }
        public Guid PointDefenseSystemPowerLevelGuid { get; set; }
        public string Class { get; set; }
        public int HullMass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseHullHealth { get; set; }
        public string Description { get; set; }
        public int BaseCrewCost { get; set; }
        public int ModulePointLimit { get; set; }
        public int FighterBays { get; set; }
        public int CorvetteBays { get; set; }
        public int FrigateBays { get; set; }
        public int DestroyerBays { get; set; }

        public virtual PowerLevel PointDefenseSystemPowerLevel { get; set; }
        public virtual SpaceshipCategory SpaceshipCategory { get; set; }
        public virtual SpaceshipType SpaceshipType { get; set; }
        public virtual PowerLevel WeaponPowerLevel { get; set; }
        public virtual ICollection<FleetCarrier> FleetCarriers { get; set; }
    }
}
