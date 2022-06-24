using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Spaceship
    {
        public Guid SpaceshipGuid { get; set; }
        public Guid SpaceshipTypeGuid { get; set; }
        public Guid SpaceshipCategoryGuid { get; set; }
        public Guid WeaponPowerLevelGuid { get; set; }
        public Guid PointDefenseSystemPowerLevelGuid { get; set; }
        public string Class { get; set; } = null!;
        public int HullMass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseHullHealth { get; set; }
        public string Description { get; set; } = null!;

        public virtual PowerLevel PointDefenseSystemPowerLevel { get; set; } = null!;
        public virtual SpaceshipCategory SpaceshipCategory { get; set; } = null!;
        public virtual SpaceshipType SpaceshipType { get; set; } = null!;
        public virtual PowerLevel WeaponPowerLevel { get; set; } = null!;
    }
}
