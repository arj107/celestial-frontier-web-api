namespace celestial_frontier_web_api.Models.DTOs {
    public class WeaponFrameTypeDto {
        public WeaponFrameTypeDto() {}
        public WeaponFrameTypeDto(WeaponFrameType weaponFrameType) {
            this.WeaponFrameTypeGuid = weaponFrameType.WeaponFrameTypeGuid;
            this.Text = weaponFrameType.Text;
        }

        public Guid WeaponFrameTypeGuid {
            get; set;
        }
        public string Text { get; set; } = null!;
    }
}
