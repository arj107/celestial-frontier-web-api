using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetFrigate
    {
        public Guid FleetFrigateGuid { get; set; }
        public Guid FrigateConfigurationGuid { get; set; }
        public Guid FleetCarrierGuid { get; set; }

        public virtual FleetCarrier FleetCarrier { get; set; }
        public virtual FrigateConfiguration FrigateConfiguration { get; set; }
    }
}
