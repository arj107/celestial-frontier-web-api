namespace celestial_frontier_web_api.Models {
    public partial class FleetFrigate
    {
        public Guid FleetFrigateGuid { get; set; }
        public Guid FleetGuid { get; set; }
        public Guid FrigateGuid { get; set; }
        public Guid WeaponFrameGuid { get; set; }
        public Guid PayloadGuid { get; set; }
        public Guid ArmorGuid { get; set; }
        public Guid PointDefenseSystemGuid { get; set; }
        public Guid FuelSystemGuid { get; set; }
        public Guid ThrusterGuid { get; set; }

        public virtual Armor Armor { get; set; }
        public virtual Fleet Fleet { get; set; }
        public virtual Frigate Frigate { get; set; }
        public virtual FuelSystem FuelSystem { get; set; }
        public virtual Payload Payload { get; set; }
        public virtual PointDefenseSystem PointDefenseSystem { get; set; }
        public virtual Thruster Thruster { get; set; }
        public virtual WeaponFrame WeaponFrame { get; set; }
    }
}
