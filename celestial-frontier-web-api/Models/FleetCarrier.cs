using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetCarrier
    {
        public Guid FleetCarrierGuid { get; set; }
        public Guid CarrierConfigurationGuid { get; set; }
        public Guid FleetGuid { get; set; }

        public virtual CarrierConfiguration CarrierConfiguration { get; set; }
        public virtual Fleet Fleet { get; set; }
    }
}
