using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Payload
    {
        public Guid PayloadGuid { get; set; }
        public Guid PayloadTypeGuid { get; set; }
        public Guid WeaponFrameTypeGuid { get; set; }
        public Guid PowerLevelGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public double KineticDamageBias { get; set; }
        public double CounterSusceptibility { get; set; }
        public int BaseDamage { get; set; }
        public double BaseCriticalHitMultiplier { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual PayloadType PayloadType { get; set; } = null!;
        public virtual PowerLevel PowerLevel { get; set; } = null!;
        public virtual WeaponFrameType WeaponFrameType { get; set; } = null!;
    }
}
