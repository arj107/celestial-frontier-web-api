using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetCorvette
    {
        public Guid FleetCorvetteGuid { get; set; }
        public Guid CorvetteConfigurationGuid { get; set; }
        public Guid FleetGuid { get; set; }

        public virtual CorvetteConfiguration CorvetteConfiguration { get; set; }
        public virtual Fleet Fleet { get; set; }
    }
}
