namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetDestroyerDto {
        public Guid FleetDestroyerGuid {
            get; set;
        }
        public virtual DestroyerConfigurationDto DestroyerConfiguration {
            get; set;
        }
        public virtual FleetDto Fleet {
            get; set;
        }
    }
}
