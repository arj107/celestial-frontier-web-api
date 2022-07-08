using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetFighter
    {
        public Guid FleetFighterGuid { get; set; }
        public Guid FighterConfigurationGuid { get; set; }
        public Guid FleetGuid { get; set; }

        public virtual FighterConfiguration FighterConfiguration { get; set; }
        public virtual Fleet Fleet { get; set; }
    }
}
