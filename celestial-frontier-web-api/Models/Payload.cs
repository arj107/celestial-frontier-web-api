using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Payload
    {
        public Payload()
        {
            FleetBattlecruiserPayload1s = new HashSet<FleetBattlecruiser>();
            FleetBattlecruiserPayload2s = new HashSet<FleetBattlecruiser>();
            FleetCarriers = new HashSet<FleetCarrier>();
            FleetCorvettes = new HashSet<FleetCorvette>();
            FleetDestroyerPayload1s = new HashSet<FleetDestroyer>();
            FleetDestroyerPayload2s = new HashSet<FleetDestroyer>();
            FleetFighters = new HashSet<FleetFighter>();
            FleetFrigates = new HashSet<FleetFrigate>();
        }

        public Guid PayloadGuid { get; set; }
        public Guid PayloadTypeGuid { get; set; }
        public Guid WeaponFrameTypeGuid { get; set; }
        public Guid PowerLevelGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public double Velocity { get; set; }
        public double KineticDamageBias { get; set; }
        public double CounterSusceptibility { get; set; }
        public int BaseDamage { get; set; }
        public double BaseCriticalHitMultiplier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual PayloadType PayloadType { get; set; }
        public virtual PowerLevel PowerLevel { get; set; }
        public virtual WeaponFrameType WeaponFrameType { get; set; }
        public virtual ICollection<FleetBattlecruiser> FleetBattlecruiserPayload1s { get; set; }
        public virtual ICollection<FleetBattlecruiser> FleetBattlecruiserPayload2s { get; set; }
        public virtual ICollection<FleetCarrier> FleetCarriers { get; set; }
        public virtual ICollection<FleetCorvette> FleetCorvettes { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyerPayload1s { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyerPayload2s { get; set; }
        public virtual ICollection<FleetFighter> FleetFighters { get; set; }
        public virtual ICollection<FleetFrigate> FleetFrigates { get; set; }
    }
}
