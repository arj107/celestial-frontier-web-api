namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetBattlecruiserDto {
        public Guid FleetBattlecruiserGuid {
            get; set;
        }

        public virtual BattlecruiserConfigurationDto BattlecruiserConfiguration {
            get; set;
        }
        public virtual FleetDto Fleet {
            get; set;
        }
    }
}
