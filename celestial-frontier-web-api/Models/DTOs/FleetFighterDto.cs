namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetFighterDto {
        public Guid FleetFighterGuid {
            get; set;
        }
        public virtual FighterConfigurationDto FighterConfiguration {
            get; set;
        }
        public virtual FleetDto Fleet {
            get; set;
        }
    }
}
