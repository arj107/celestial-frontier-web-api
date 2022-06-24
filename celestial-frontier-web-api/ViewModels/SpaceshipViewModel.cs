namespace celestial_frontier_web_api.ViewModels {
    public class SpaceshipViewModel {
        public Guid SpaceshipGuid {
            get; set;
        }
        public Guid SpaceshipTypeGuid {
            get; set;
        }
        public Guid SpaceshipCategoryGuid {
            get; set;
        }
        public Guid WeaponPowerLevelGuid {
            get; set;
        }
        public Guid PointDefenseSystemPowerLevelGuid {
            get; set;
        }
        public string Class { get; set; } = null!;
        public int HullMass {
            get; set;
        }
        public int BaseHullHealth {
            get; set;
        }
        public string Description { get; set; } = null!;

        public string PointDefenseSystemPowerLevel { get; set; } = null!;
        public string SpaceshipCategory { get; set; } = null!;
        public string SpaceshipType { get; set; } = null!;
        public string WeaponPowerLevel { get; set; } = null!;
    }
}
