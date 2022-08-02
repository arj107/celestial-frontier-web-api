namespace celestial_frontier_web_api.Models.DTOs {
    public class CorvetteConfigurationDto {
        public Guid CorvetteConfigurationGuid {
            get; set;
        }
        public ArmorDto Armor {
            get; set;
        }
        public CorvetteDto Corvette {
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
        public UserDto User {
            get; set;
        }
        public WeaponFrameDto WeaponFrame {
            get; set;
        }
        public string Name {
            get; set;
        }
    }
}
