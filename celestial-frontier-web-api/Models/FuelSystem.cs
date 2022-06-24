using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FuelSystem
    {
        public Guid FuelSystemGuid { get; set; }
        public Guid SpaceshipTypeGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public double FuelCapacity { get; set; }
        public double FuelRegenerationRate { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual SpaceshipType SpaceshipType { get; set; } = null!;
    }
}
