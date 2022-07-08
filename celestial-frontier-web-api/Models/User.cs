using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class User
    {
        public User()
        {
            BattlecruiserConfigurations = new HashSet<BattlecruiserConfiguration>();
            CarrierConfigurations = new HashSet<CarrierConfiguration>();
            CorvetteConfigurations = new HashSet<CorvetteConfiguration>();
            DestroyerConfigurations = new HashSet<DestroyerConfiguration>();
            FighterConfigurations = new HashSet<FighterConfiguration>();
            Fleets = new HashSet<Fleet>();
            FrigateConfigurations = new HashSet<FrigateConfiguration>();
        }

        public Guid UserGuid { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<BattlecruiserConfiguration> BattlecruiserConfigurations { get; set; }
        public virtual ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }
        public virtual ICollection<CorvetteConfiguration> CorvetteConfigurations { get; set; }
        public virtual ICollection<DestroyerConfiguration> DestroyerConfigurations { get; set; }
        public virtual ICollection<FighterConfiguration> FighterConfigurations { get; set; }
        public virtual ICollection<Fleet> Fleets { get; set; }
        public virtual ICollection<FrigateConfiguration> FrigateConfigurations { get; set; }
    }
}
