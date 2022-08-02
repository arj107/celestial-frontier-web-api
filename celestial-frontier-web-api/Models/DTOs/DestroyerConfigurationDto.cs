namespace celestial_frontier_web_api.Models.DTOs {
    public class DestroyerConfigurationDto {
        public Guid DestroyerConfigurationGuid {
            get; set;
        }
        public ArmorDto Armor {
            get; set;
        }
        public DestroyerDto Destroyer {
            get; set;
        }
        public FuelSystemDto FuelSystem {
            get; set;
        }
        public PayloadDto Payload1 {
            get; set;
        }
        public PayloadDto Payload2 {
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
        public WeaponFrameDto WeaponFrame1 {
            get; set;
        }
        public WeaponFrameDto WeaponFrame2 {
            get; set;
        }
        public string Name {
            get; set;
        }
    }
}
