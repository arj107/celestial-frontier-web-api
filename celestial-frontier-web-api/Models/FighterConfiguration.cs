using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FighterConfiguration
    {
        public FighterConfiguration()
        {
            FleetFighters = new HashSet<FleetFighter>();
        }

        public Guid FighterConfigurationGuid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid FighterGuid { get; set; }
        public Guid WeaponFrameGuid { get; set; }
        public Guid PayloadGuid { get; set; }
        public Guid ArmorGuid { get; set; }
        public Guid PointDefenseSystemGuid { get; set; }
        public Guid FuelSystemGuid { get; set; }
        public Guid ThrusterGuid { get; set; }
        public string Name { get; set; }

        public virtual Armor Armor { get; set; }
        public virtual Fighter Fighter { get; set; }
        public virtual FuelSystem FuelSystem { get; set; }
        public virtual Payload Payload { get; set; }
        public virtual PointDefenseSystem PointDefenseSystem { get; set; }
        public virtual Thruster Thruster { get; set; }
        public virtual User User { get; set; }
        public virtual WeaponFrame WeaponFrame { get; set; }
        public virtual ICollection<FleetFighter> FleetFighters { get; set; }
    }
}
