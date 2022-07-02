namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetFrigateDto {
        public ArmorDto Armor {
            get; set;
        }
        public CarrierDto Carrier {
            get; set;
        }
        public FleetDto Fleet {
            get; set;
        }
        public FuelSystemDto FuelSystem {
            get; set;
        }
        public PayloadDto Payload {
            get; set;
        }
        public PointDefenseSystemDto PointDefenseSystem {
            get; set;
        }
        public ThrusterDto Thruster {
            get; set;
        }
        public WeaponFrameDto WeaponFrame {
            get; set;
        }
    }
}
