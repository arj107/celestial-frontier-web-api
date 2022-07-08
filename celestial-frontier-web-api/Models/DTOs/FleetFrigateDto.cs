namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetFrigateDto {
        public Guid FleetFrigateGuid {
            get; set;
        }
        public virtual FrigateConfigurationDto FrigateConfiguration {
            get; set;
        }
        public virtual FleetDto Fleet {
            get; set;
        }
    }
}
