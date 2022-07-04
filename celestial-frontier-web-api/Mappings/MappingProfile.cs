using AutoMapper;
using celestial_frontier_web_api.Models;
using celestial_frontier_web_api.Models.DTOs;

namespace celestial_frontier_web_api.Mappings {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Armor, ArmorDto>();
            CreateMap<ArmorDto, Armor>();
            CreateMap<Battlecruiser, BattlecruiserDto>();
            CreateMap<BattlecruiserDto, Battlecruiser>();
            CreateMap<Carrier, CarrierDto>();
            CreateMap<CarrierDto, Carrier>();
            CreateMap<Destroyer, DestroyerDto>();
            CreateMap<DestroyerDto, Destroyer>();
            CreateMap<Fighter, FighterDto>();
            CreateMap<FighterDto, Fighter>();
            CreateMap<BattlecruiserConfiguration, BattlecruiserConfigurationDto>();
            CreateMap<BattlecruiserConfigurationDto, BattlecruiserConfiguration>();
            CreateMap<CarrierConfiguration, CarrierConfigurationDto>();
            CreateMap<CarrierConfigurationDto, CarrierConfiguration>();
            CreateMap<DestroyerConfiguration, DestroyerConfigurationDto>();
            CreateMap<DestroyerConfigurationDto, DestroyerConfiguration>();
            CreateMap<FleetBattlecruiser, FleetBattlecruiserDto>();
            CreateMap<FleetBattlecruiserDto, FleetBattlecruiser>();
            CreateMap<FleetCarrier, FleetCarrierDto>();
            CreateMap<FleetCarrierDto, FleetCarrier>();
            CreateMap<FleetCorvette, FleetCorvetteDto>();
            CreateMap<FleetCorvetteDto, FleetCorvette>();
            CreateMap<FleetDestroyer, FleetDestroyerDto>();
            CreateMap<FleetDestroyerDto, FleetDestroyer>();
            CreateMap<FleetFighter, FleetFighterDto>();
            CreateMap<FleetFighterDto, FleetFighter>();
            CreateMap<FleetFrigate, FleetFrigateDto>();
            CreateMap<FleetFrigateDto, FleetFrigate>();
            CreateMap<Fleet, FleetDto>();
            CreateMap<FleetDto, Fleet>();
            CreateMap<FighterConfiguration, FighterConfigurationDto>();
            CreateMap<FighterConfigurationDto, FighterConfiguration>();
            CreateMap<FrigateConfiguration, FrigateConfigurationDto>();
            CreateMap<FrigateConfigurationDto, FrigateConfiguration>();
            CreateMap<Frigate, FrigateDto>();
            CreateMap<FrigateDto, Frigate>();
            CreateMap<FuelSystem, FuelSystemDto>();
            CreateMap<FuelSystemDto, FuelSystem>();
            CreateMap<Payload, PayloadDto>();
            CreateMap<PayloadDto, Payload>();
            CreateMap<PayloadType, PayloadTypeDto>();
            CreateMap<PayloadTypeDto, PayloadType>();
            CreateMap<PointDefenseSystem, PointDefenseSystemDto>();
            CreateMap<PointDefenseSystemDto, PointDefenseSystem>();
            CreateMap<PowerLevel, PowerLevelDto>();
            CreateMap<PowerLevelDto, PowerLevel>();
            CreateMap<SpaceshipCategory, SpaceshipCategoryDto>();
            CreateMap<SpaceshipCategoryDto, SpaceshipCategory>();
            CreateMap<SpaceshipType, SpaceshipTypeDto>();
            CreateMap<SpaceshipTypeDto, SpaceshipType>();
            CreateMap<Thruster, ThrusterDto>();
            CreateMap<ThrusterDto, Thruster>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<WeaponFrame, WeaponFrameDto>();
            CreateMap<WeaponFrameDto, WeaponFrame>();
            CreateMap<WeaponFrameType, WeaponFrameTypeDto>();
            CreateMap<WeaponFrameTypeDto, WeaponFrameType>();
        }
    }
}
