namespace celestial_frontier_web_api.Models.DTOs {
    public class PayloadDto {
        public Guid PayloadGuid { get; set; }
        public PayloadTypeDto PayloadType { get; set; } = null!;
        public WeaponFrameTypeDto WeaponFrameType { get; set; } = null!;
        public PowerLevelDto PowerLevel { get; set; } = null!;
        public int ModulePointsCost { get; set; }
        public double KineticDamageBias { get; set; }
        public double CounterSusceptibility { get; set; }
        public int BaseDamage { get; set; }
        public double BaseCriticalHitMultiplier { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public PayloadDto() {}
        public PayloadDto(Payload payload) {
            this.PayloadGuid = payload.PayloadGuid;
            this.PayloadType = new PayloadTypeDto(payload.PayloadType);
            this.WeaponFrameType = new WeaponFrameTypeDto(payload.WeaponFrameType);
            this.PowerLevel = new PowerLevelDto(payload.PowerLevel);
            this.ModulePointsCost = payload.ModulePointsCost;
            this.KineticDamageBias = payload.KineticDamageBias;
            this.CounterSusceptibility = payload.CounterSusceptibility;
            this.BaseDamage = payload.BaseDamage;
            this.BaseCriticalHitMultiplier = payload.BaseCriticalHitMultiplier;
            this.Title = payload.Title;
            this.Description = payload.Description;
        }
    }
}
