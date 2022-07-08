namespace celestial_frontier_web_api.Models.DTOs {
    public class FrigateDto {
        public Guid FrigateGuid {
            get; set;
        }
        public SpaceshipTypeDto SpaceshipType {
            get; set;
        }
        public SpaceshipCategoryDto SpaceshipCategory {
            get; set;
        }
        public PowerLevelDto WeaponPowerLevel {
            get; set;
        }
        public PowerLevelDto PointDefenseSystemPowerLevel {
            get; set;
        }
        public string Class {
            get; set;
        }
        public int HullMass {
            get; set;
        }
        public double SpecificHeatCapacity {
            get; set;
        }
        public int BaseHullHealth {
            get; set;
        }
        public string Description {
            get; set;
        }
        public int BaseVigorCost {
            get; set;
        }
        public int BaseCrewCost {
            get; set;
        }
        public int ModulePointLimit {
            get; set;
        }
    }
}
