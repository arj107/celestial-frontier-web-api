using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Fleet
    {
        public Guid FleetGuid { get; set; }
        public Guid? FleetBattlecruiserGuid { get; set; }
        public Guid? FleetCarrier1Guid { get; set; }
        public Guid? FleetCarrier2Guid { get; set; }
        public Guid UserGuid { get; set; }
        public string Name { get; set; }

        public virtual FleetCarrier FleetCarrier1 { get; set; }
        public virtual FleetCarrier FleetCarrier2 { get; set; }
        public virtual User User { get; set; }
    }
}
