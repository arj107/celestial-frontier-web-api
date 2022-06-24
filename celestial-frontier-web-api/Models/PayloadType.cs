using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class PayloadType
    {
        public PayloadType()
        {
            Payloads = new HashSet<Payload>();
        }

        public Guid PayloadTypeGuid { get; set; }
        public string Text { get; set; } = null!;

        public virtual ICollection<Payload> Payloads { get; set; }
    }
}
