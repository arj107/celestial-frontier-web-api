using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetBattlecruiser
    {
        public Guid FleetBattlecruiserGuid { get; set; }
        public Guid FleetGuid { get; set; }
        public Guid BattlecruiserGuid { get; set; }
        public Guid WeaponFrame1Guid { get; set; }
        public Guid Payload1Guid { get; set; }
        public Guid WeaponFrame2Guid { get; set; }
        public Guid Payload2Guid { get; set; }
        public Guid ArmorGuid { get; set; }
        public Guid PointDefenseSystemGuid { get; set; }
        public Guid FuelSystemGuid { get; set; }
        public Guid ThrusterGuid { get; set; }

        public virtual Armor Armor { get; set; }
        public virtual Battlecruiser Battlecruiser { get; set; }
        public virtual Fleet Fleet { get; set; }
        public virtual FuelSystem FuelSystem { get; set; }
        public virtual Payload Payload1 { get; set; }
        public virtual Payload Payload2 { get; set; }
        public virtual PointDefenseSystem PointDefenseSystem { get; set; }
        public virtual Thruster Thruster { get; set; }
        public virtual WeaponFrame WeaponFrame1 { get; set; }
        public virtual WeaponFrame WeaponFrame2 { get; set; }
    }
}
