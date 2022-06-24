using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class WeaponFrame
    {
        public Guid WeaponFrameGuid { get; set; }
        public Guid WeaponFrameTypeGuid { get; set; }
        public Guid PowerLevelGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseMagSize { get; set; }
        public double BaseAccuracyModifier { get; set; }
        public int BaseTurnsToArm { get; set; }
        public double BaseCriticalHitChance { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual PowerLevel PowerLevel { get; set; } = null!;
        public virtual WeaponFrameType WeaponFrameType { get; set; } = null!;
    }
}
