using System;
using System.Collections.Generic;

namespace celestial_frontier_web_api.Models
{
    public partial class WeaponFrameType
    {
        public WeaponFrameType()
        {
            Payloads = new HashSet<Payload>();
            WeaponFrames = new HashSet<WeaponFrame>();
        }

        public Guid WeaponFrameTypeGuid { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Payload> Payloads { get; set; }
        public virtual ICollection<WeaponFrame> WeaponFrames { get; set; }
    }
}
