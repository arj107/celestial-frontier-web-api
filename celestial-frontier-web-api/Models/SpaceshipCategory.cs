using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class SpaceshipCategory
    {
        public SpaceshipCategory()
        {
            Spaceships = new HashSet<Spaceship>();
        }

        public Guid SpaceshipCategoryGuid { get; set; }
        public string Text { get; set; } = null!;

        public virtual ICollection<Spaceship> Spaceships { get; set; }
    }
}
