namespace celestial_frontier_web_api.Models.DTOs {
    public class PayloadTypeDto {
        public PayloadTypeDto() {}

        public PayloadTypeDto(PayloadType payloadType) {
            this.PayloadTypeGuid = payloadType.PayloadTypeGuid;
            this.Text = payloadType.Text;
        }

        public Guid PayloadTypeGuid {
            get; set;
        }
        public string Text { get; set; } = null!;
    }
}
