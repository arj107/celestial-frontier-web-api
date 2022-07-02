using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class PowerLevel
    {
        public PowerLevel()
        {
            BattlecruiserPointDefenseSystemPowerLevels = new HashSet<Battlecruiser>();
            BattlecruiserWeaponPowerLevels = new HashSet<Battlecruiser>();
            CarrierPointDefenseSystemPowerLevels = new HashSet<Carrier>();
            CarrierWeaponPowerLevels = new HashSet<Carrier>();
            CorvettePointDefenseSystemPowerLevels = new HashSet<Corvette>();
            CorvetteWeaponPowerLevels = new HashSet<Corvette>();
            DestroyerPointDefenseSystemPowerLevels = new HashSet<Destroyer>();
            DestroyerWeaponPowerLevels = new HashSet<Destroyer>();
            FighterPointDefenseSystemPowerLevels = new HashSet<Fighter>();
            FighterWeaponPowerLevels = new HashSet<Fighter>();
            FrigatePointDefenseSystemPowerLevels = new HashSet<Frigate>();
            FrigateWeaponPowerLevels = new HashSet<Frigate>();
            Payloads = new HashSet<Payload>();
            PointDefenseSystems = new HashSet<PointDefenseSystem>();
            SpaceshipPointDefenseSystemPowerLevels = new HashSet<Spaceship>();
            SpaceshipWeaponPowerLevels = new HashSet<Spaceship>();
            WeaponFrames = new HashSet<WeaponFrame>();
        }

        public Guid PowerLevelGuid { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Battlecruiser> BattlecruiserPointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Battlecruiser> BattlecruiserWeaponPowerLevels { get; set; }
        public virtual ICollection<Carrier> CarrierPointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Carrier> CarrierWeaponPowerLevels { get; set; }
        public virtual ICollection<Corvette> CorvettePointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Corvette> CorvetteWeaponPowerLevels { get; set; }
        public virtual ICollection<Destroyer> DestroyerPointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Destroyer> DestroyerWeaponPowerLevels { get; set; }
        public virtual ICollection<Fighter> FighterPointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Fighter> FighterWeaponPowerLevels { get; set; }
        public virtual ICollection<Frigate> FrigatePointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Frigate> FrigateWeaponPowerLevels { get; set; }
        public virtual ICollection<Payload> Payloads { get; set; }
        public virtual ICollection<PointDefenseSystem> PointDefenseSystems { get; set; }
        public virtual ICollection<Spaceship> SpaceshipPointDefenseSystemPowerLevels { get; set; }
        public virtual ICollection<Spaceship> SpaceshipWeaponPowerLevels { get; set; }
        public virtual ICollection<WeaponFrame> WeaponFrames { get; set; }
    }
}
