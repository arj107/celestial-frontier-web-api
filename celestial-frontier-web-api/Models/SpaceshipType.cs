using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class SpaceshipType
    {
        public SpaceshipType()
        {
            Armors = new HashSet<Armor>();
            Battlecruisers = new HashSet<Battlecruiser>();
            Carriers = new HashSet<Carrier>();
            Corvettes = new HashSet<Corvette>();
            Destroyers = new HashSet<Destroyer>();
            Fighters = new HashSet<Fighter>();
            Frigates = new HashSet<Frigate>();
            FuelSystems = new HashSet<FuelSystem>();
            Spaceships = new HashSet<Spaceship>();
            Thrusters = new HashSet<Thruster>();
        }

        public Guid SpaceshipTypeGuid { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Armor> Armors { get; set; }
        public virtual ICollection<Battlecruiser> Battlecruisers { get; set; }
        public virtual ICollection<Carrier> Carriers { get; set; }
        public virtual ICollection<Corvette> Corvettes { get; set; }
        public virtual ICollection<Destroyer> Destroyers { get; set; }
        public virtual ICollection<Fighter> Fighters { get; set; }
        public virtual ICollection<Frigate> Frigates { get; set; }
        public virtual ICollection<FuelSystem> FuelSystems { get; set; }
        public virtual ICollection<Spaceship> Spaceships { get; set; }
        public virtual ICollection<Thruster> Thrusters { get; set; }
    }
}
