namespace celestial_frontier_web_api.Models.DTOs {
    public class FleetDto {
        public Guid FleetGuid { get; set; }
        public UserDto User { get; set; }
        public string Name { get; set; }
        public ICollection<FleetBattlecruiserDto> FleetBattlecruisers { get; set; }
        public ICollection<FleetCarrierDto> FleetCarriers { get; set; }
        public ICollection<FleetCorvetteDto> FleetCorvettes { get; set; }
        public ICollection<FleetDestroyerDto> FleetDestroyers { get; set; }
        public ICollection<FleetFighterDto> FleetFighters { get; set; }
        public ICollection<FleetFrigateDto> FleetFrigates { get; set; }

        public FleetDto() {}
        public FleetDto(Fleet fleet) {
            this.FleetGuid = fleet.FleetGuid;
            this.User = new UserDto(fleet.User);
            this.Name = fleet.Name;
        }
    }
}
