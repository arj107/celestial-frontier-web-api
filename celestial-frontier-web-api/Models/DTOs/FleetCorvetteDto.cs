namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetCorvetteDto {
        public Guid FleetCorvetteGuid {
            get; set;
        }
        public virtual CorvetteConfigurationDto CorvetteConfiguration {
            get; set;
        }
        public virtual FleetDto Fleet {
            get; set;
        }
    }
}
