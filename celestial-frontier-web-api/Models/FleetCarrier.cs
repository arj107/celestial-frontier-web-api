using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetCarrier
    {
        public FleetCarrier()
        {
            FleetCorvettes = new HashSet<FleetCorvette>();
            FleetDestroyers = new HashSet<FleetDestroyer>();
            FleetFighters = new HashSet<FleetFighter>();
            FleetFleetCarrier1s = new HashSet<Fleet>();
            FleetFleetCarrier2s = new HashSet<Fleet>();
            FleetFrigates = new HashSet<FleetFrigate>();
        }

        public Guid FleetCarrierGuid { get; set; }
        public Guid CarrierConfigurationGuid { get; set; }

        public virtual CarrierConfiguration CarrierConfiguration { get; set; }
        public virtual ICollection<FleetCorvette> FleetCorvettes { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyers { get; set; }
        public virtual ICollection<FleetFighter> FleetFighters { get; set; }
        public virtual ICollection<Fleet> FleetFleetCarrier1s { get; set; }
        public virtual ICollection<Fleet> FleetFleetCarrier2s { get; set; }
        public virtual ICollection<FleetFrigate> FleetFrigates { get; set; }
    }
}
