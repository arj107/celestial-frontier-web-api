using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class WeaponFrame
    {
        public WeaponFrame()
        {
            FleetBattlecruiserWeaponFrame1s = new HashSet<FleetBattlecruiser>();
            FleetBattlecruiserWeaponFrame2s = new HashSet<FleetBattlecruiser>();
            FleetCarriers = new HashSet<FleetCarrier>();
            FleetCorvettes = new HashSet<FleetCorvette>();
            FleetDestroyerWeaponFrame1s = new HashSet<FleetDestroyer>();
            FleetDestroyerWeaponFrame2s = new HashSet<FleetDestroyer>();
            FleetFighters = new HashSet<FleetFighter>();
            FleetFrigates = new HashSet<FleetFrigate>();
        }

        public Guid WeaponFrameGuid { get; set; }
        public Guid WeaponFrameTypeGuid { get; set; }
        public Guid PowerLevelGuid { get; set; }
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseMagSize { get; set; }
        public double TargetingEfficiency { get; set; }
        public int OptimalRange { get; set; }
        public int EffectiveRange { get; set; }
        public double BaseCriticalHitChance { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual PowerLevel PowerLevel { get; set; }
        public virtual WeaponFrameType WeaponFrameType { get; set; }
        public virtual ICollection<FleetBattlecruiser> FleetBattlecruiserWeaponFrame1s { get; set; }
        public virtual ICollection<FleetBattlecruiser> FleetBattlecruiserWeaponFrame2s { get; set; }
        public virtual ICollection<FleetCarrier> FleetCarriers { get; set; }
        public virtual ICollection<FleetCorvette> FleetCorvettes { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyerWeaponFrame1s { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyerWeaponFrame2s { get; set; }
        public virtual ICollection<FleetFighter> FleetFighters { get; set; }
        public virtual ICollection<FleetFrigate> FleetFrigates { get; set; }
    }
}
