using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class SpaceshipType
    {
        public SpaceshipType()
        {
            Armors = new HashSet<Armor>();
            FuelSystems = new HashSet<FuelSystem>();
            Spaceships = new HashSet<Spaceship>();
            Thrusters = new HashSet<Thruster>();
        }

        public Guid SpaceshipTypeGuid { get; set; }
        public string Text { get; set; } = null!;

        public virtual ICollection<Armor> Armors { get; set; }
        public virtual ICollection<FuelSystem> FuelSystems { get; set; }
        public virtual ICollection<Spaceship> Spaceships { get; set; }
        public virtual ICollection<Thruster> Thrusters { get; set; }
    }
}
