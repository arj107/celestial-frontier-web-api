using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class PointDefenseSystem
    {
        public Guid PointDefenseSystemGuid { get; set; }
        public Guid PowerLevelGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int CountermeasureCapacity { get; set; }
        public int CountermeasureRegenerationRate { get; set; }
        public double Efficiency { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual PowerLevel PowerLevel { get; set; } = null!;
    }
}
