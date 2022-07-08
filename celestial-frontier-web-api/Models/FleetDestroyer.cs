using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class FleetDestroyer
    {
        public Guid FleetDestroyerGuid { get; set; }
        public Guid DestroyerConfigurationGuid { get; set; }
        public Guid FleetGuid { get; set; }

        public virtual DestroyerConfiguration DestroyerConfiguration { get; set; }
        public virtual Fleet Fleet { get; set; }
    }
}
