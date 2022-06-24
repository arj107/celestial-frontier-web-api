using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class PowerLevel
    {
        public PowerLevel()
        {
            Payloads = new HashSet<Payload>();
            PointDefenseSystems = new HashSet<PointDefenseSystem>();
            SpaceshipPointDefenseSystemPowerLevels = new HashSet<Spaceship>();
            SpaceshipWeaponPowerLevels = new HashSet<Spaceship>();
            WeaponFrames = new HashSet<WeaponFrame>();
        }

        public Guid PowerLevelGuid { get; set; }
        public string Text { get; set; } = null!;

        public virtual ICollection<Payload> Payloads { get; set; }
        public virtual ICollection<PointDefenseSystem> PointDefenseSystems { get; set; }
        public virtual ICollection<Spaceship> SpaceshipPointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Spaceship> SpaceshipWeaponPowerLevels { get; set; }
        public virtual ICollection<WeaponFrame> WeaponFrames { get; set; }
    }
}
