using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Thruster
    {
        public Guid ThrusterGuid { get; set; }
        public Guid SpaceshipTypeGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public double Thrust { get; set; }
        public double Efficiency { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual SpaceshipType SpaceshipType { get; set; } = null!;
    }
}
