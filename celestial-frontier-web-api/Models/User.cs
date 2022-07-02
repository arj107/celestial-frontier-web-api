using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class User {
        public User() {
            Fleets = new HashSet<Fleet>();
        }
        public Guid UserGuid { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        [JsonIgnore]
        public virtual ICollection<Fleet> Fleets {
            get; set;
        }
    }
}
