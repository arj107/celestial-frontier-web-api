namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetCarrierDto {
        public Guid FleetCarrierGuid {
            get; set;
        }

        public virtual CarrierConfigurationDto CarrierConfiguration {
            get; set;
        }
        public virtual FleetDto Fleet {
            get; set;
        }
    }
}
