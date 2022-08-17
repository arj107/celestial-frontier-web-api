using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetBattlecruiser
    {
        public Guid FleetBattlecruiserGuid { get; set; }
        public Guid BattlecruiserConfigurationGuid { get; set; }

        public virtual BattlecruiserConfiguration BattlecruiserConfiguration { get; set; }
    }
}
