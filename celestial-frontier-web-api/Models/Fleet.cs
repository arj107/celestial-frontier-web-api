using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class Fleet
    {
        public Fleet()
        {
            FleetBattlecruisers = new HashSet<FleetBattlecruiser>();
            FleetCarriers = new HashSet<FleetCarrier>();
            FleetCorvettes = new HashSet<FleetCorvette>();
            FleetDestroyers = new HashSet<FleetDestroyer>();
            FleetFighters = new HashSet<FleetFighter>();
            FleetFrigates = new HashSet<FleetFrigate>();
        }

        public Guid FleetGuid { get; set; }
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public int CrewCapacity { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<FleetBattlecruiser> FleetBattlecruisers { get; set; }
        public virtual ICollection<FleetCarrier> FleetCarriers { get; set; }
        public virtual ICollection<FleetCorvette> FleetCorvettes { get; set; }
        public virtual ICollection<FleetDestroyer> FleetDestroyers { get; set; }
        public virtual ICollection<FleetFighter> FleetFighters { get; set; }
        public virtual ICollection<FleetFrigate> FleetFrigates { get; set; }
    }
}
