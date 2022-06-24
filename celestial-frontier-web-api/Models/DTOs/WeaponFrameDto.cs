namespace celestial_frontier_web_api.Models.DTOs {
    public class WeaponFrameDto {
        public Guid WeaponFrameGuid { get; set; }
        public WeaponFrameTypeDto WeaponFrameType { get; set; } = null!;
        public PowerLevelDto PowerLevel { get; set; } = null!;
        public int ModulePointsCost { get; set; }
        public int Mass { get; set; }
        public double SpecificHeatCapacity { get; set; }
        public int BaseMagSize { get; set; }
        public double BaseAccuracyModifier { get; set; }
        public int BaseTurnsToArm { get; set; }
        public double BaseCriticalHitChance { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public WeaponFrameDto(WeaponFrame weaponFrame) {
            this.WeaponFrameGuid = weaponFrame.WeaponFrameGuid;
            this.WeaponFrameType = new WeaponFrameTypeDto(weaponFrame.WeaponFrameType);
            this.PowerLevel = new PowerLevelDto(weaponFrame.PowerLevel);
            this.ModulePointsCost = weaponFrame.ModulePointsCost;
            this.Mass = weaponFrame.Mass;
            this.SpecificHeatCapacity = weaponFrame.SpecificHeatCapacity;
            this.BaseMagSize = weaponFrame.BaseMagSize;
            this.BaseAccuracyModifier = weaponFrame.BaseAccuracyModifier;
            this.BaseTurnsToArm = weaponFrame.BaseTurnsToArm;
            this.BaseCriticalHitChance = weaponFrame.BaseCriticalHitChance;
            this.Title = weaponFrame.Title;
            this.Description = weaponFrame.Description;
        }
    }
}
