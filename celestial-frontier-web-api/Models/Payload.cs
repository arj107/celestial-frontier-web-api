using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Payload
    {
        public Payload()
        {
            BattlecruiserConfigurationPayload1s = new HashSet<BattlecruiserConfiguration>();
            BattlecruiserConfigurationPayload2s = new HashSet<BattlecruiserConfiguration>();
            CarrierConfigurations = new HashSet<CarrierConfiguration>();
            CorvetteConfigurations = new HashSet<CorvetteConfiguration>();
            DestroyerConfigurationPayload1s = new HashSet<DestroyerConfiguration>();
            DestroyerConfigurationPayload2s = new HashSet<DestroyerConfiguration>();
            FighterConfigurations = new HashSet<FighterConfiguration>();
            FrigateConfigurations = new HashSet<FrigateConfiguration>();
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
        public virtual ICollection<BattlecruiserConfiguration> BattlecruiserConfigurationPayload1s { get; set; }
        public virtual ICollection<BattlecruiserConfiguration> BattlecruiserConfigurationPayload2s { get; set; }
        public virtual ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }
        public virtual ICollection<CorvetteConfiguration> CorvetteConfigurations { get; set; }
        public virtual ICollection<DestroyerConfiguration> DestroyerConfigurationPayload1s { get; set; }
        public virtual ICollection<DestroyerConfiguration> DestroyerConfigurationPayload2s { get; set; }
        public virtual ICollection<FighterConfiguration> FighterConfigurations { get; set; }
        public virtual ICollection<FrigateConfiguration> FrigateConfigurations { get; set; }
    }
}
