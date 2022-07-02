using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Destroyer
    {
        public Destroyer()
        {
            FleetDestroyers = new HashSet<FleetDestroyer>();
        }

        public Guid DestroyerGuid { get; set; }
        public Guid SpaceshipTypeGuid { get; set; }
        public Guid SpaceshipCategoryGuid { get; set; }
        public Guid WeaponPowerLevelGuid { get; set; }
        public Guid PointDefenseSystemPowerLevelGuid { get; set; }
        public string Class { get; set; }
        public int HullMass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseHullHealth { get; set; }
        public string Description { get; set; }
        public int BaseVigorCost { get; set; }
        public int BaseCrewCost { get; set; }
        public int ModulePointLimit { get; set; }

        public virtual PowerLevel PointDefenseSystemPowerLevel { get; set; }
        public virtual SpaceshipCategory SpaceshipCategory { get; set; }
        public virtual SpaceshipType SpaceshipType { get; set; }
        public virtual PowerLevel WeaponPowerLevel { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyers { get; set; }
    }
}
