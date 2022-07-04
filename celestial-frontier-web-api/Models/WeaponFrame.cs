using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class WeaponFrame
    {
        public WeaponFrame()
        {
            BattlecruiserConfigurationWeaponFrame1s = new HashSet<BattlecruiserConfiguration>();
            BattlecruiserConfigurationWeaponFrame2s = new HashSet<BattlecruiserConfiguration>();
            CarrierConfigurations = new HashSet<CarrierConfiguration>();
            CorvetteConfigurations = new HashSet<CorvetteConfiguration>();
            DestroyerConfigurationWeaponFrame1s = new HashSet<DestroyerConfiguration>();
            DestroyerConfigurationWeaponFrame2s = new HashSet<DestroyerConfiguration>();
            FighterConfigurations = new HashSet<FighterConfiguration>();
            FrigateConfigurations = new HashSet<FrigateConfiguration>();
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
        public virtual ICollection<BattlecruiserConfiguration> BattlecruiserConfigurationWeaponFrame1s { get; set; }
        public virtual ICollection<BattlecruiserConfiguration> BattlecruiserConfigurationWeaponFrame2s { get; set; }
        public virtual ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }
        public virtual ICollection<CorvetteConfiguration> CorvetteConfigurations { get; set; }
        public virtual ICollection<DestroyerConfiguration> DestroyerConfigurationWeaponFrame1s { get; set; }
        public virtual ICollection<DestroyerConfiguration> DestroyerConfigurationWeaponFrame2s { get; set; }
        public virtual ICollection<FighterConfiguration> FighterConfigurations { get; set; }
        public virtual ICollection<FrigateConfiguration> FrigateConfigurations { get; set; }
    }
}
