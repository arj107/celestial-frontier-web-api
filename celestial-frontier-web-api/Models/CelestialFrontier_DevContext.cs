using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace celestial_frontier_web_api.Models
{
    public partial class CelestialFrontier_DevContext : DbContext
    {
        public CelestialFrontier_DevContext()
        {
        }

        public CelestialFrontier_DevContext(DbContextOptions<CelestialFrontier_DevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Armor> Armors { get; set; }
        public virtual DbSet<Battlecruiser> Battlecruisers { get; set; }
        public virtual DbSet<BattlecruiserConfiguration> BattlecruiserConfigurations { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }
        public virtual DbSet<Corvette> Corvettes { get; set; }
        public virtual DbSet<CorvetteConfiguration> CorvetteConfigurations { get; set; }
        public virtual DbSet<Destroyer> Destroyers { get; set; }
        public virtual DbSet<DestroyerConfiguration> DestroyerConfigurations { get; set; }
        public virtual DbSet<Fighter> Fighters { get; set; }
        public virtual DbSet<FighterConfiguration> FighterConfigurations { get; set; }
        public virtual DbSet<Fleet> Fleets { get; set; }
        public virtual DbSet<FleetBattlecruiser> FleetBattlecruisers { get; set; }
        public virtual DbSet<FleetCarrier> FleetCarriers { get; set; }
        public virtual DbSet<FleetCorvette> FleetCorvettes { get; set; }
        public virtual DbSet<FleetDestroyer> FleetDestroyers { get; set; }
        public virtual DbSet<FleetFighter> FleetFighters { get; set; }
        public virtual DbSet<FleetFrigate> FleetFrigates { get; set; }
        public virtual DbSet<Frigate> Frigates { get; set; }
        public virtual DbSet<FrigateConfiguration> FrigateConfigurations { get; set; }
        public virtual DbSet<FuelSystem> FuelSystems { get; set; }
        public virtual DbSet<Payload> Payloads { get; set; }
        public virtual DbSet<PayloadType> PayloadTypes { get; set; }
        public virtual DbSet<PointDefenseSystem> PointDefenseSystems { get; set; }
        public virtual DbSet<PowerLevel> PowerLevels { get; set; }
        public virtual DbSet<Spaceship> Spaceships { get; set; }
        public virtual DbSet<SpaceshipCategory> SpaceshipCategories { get; set; }
        public virtual DbSet<SpaceshipType> SpaceshipTypes { get; set; }
        public virtual DbSet<Thruster> Thrusters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeaponFrame> WeaponFrames { get; set; }
        public virtual DbSet<WeaponFrameType> WeaponFrameTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=CelestialFrontier_Dev");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Armor>(entity =>
            {
                entity.HasKey(e => e.ArmorGuid);

                entity.Property(e => e.ArmorGuid).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Armors)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Armors_SpaceshipTypes");
            });

            modelBuilder.Entity<Battlecruiser>(entity =>
            {
                entity.HasKey(e => e.BattlecruiserGuid);

                entity.Property(e => e.BattlecruiserGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.BattlecruiserPointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Battlecruisers(PointDefenseSystems)_PowerLevels");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Battlecruisers)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Battlecruisers_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Battlecruisers)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Battlecruisers_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.BattlecruiserWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Battlecruisers(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<BattlecruiserConfiguration>(entity =>
            {
                entity.HasKey(e => e.BattlecruiserConfigurationGuid);

                entity.Property(e => e.BattlecruiserConfigurationGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Armor)
                    .WithMany(p => p.BattlecruiserConfigurations)
                    .HasForeignKey(d => d.ArmorGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_Armors");

                entity.HasOne(d => d.Battlecruiser)
                    .WithMany(p => p.BattlecruiserConfigurations)
                    .HasForeignKey(d => d.BattlecruiserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_Battlecruisers");

                entity.HasOne(d => d.FuelSystem)
                    .WithMany(p => p.BattlecruiserConfigurations)
                    .HasForeignKey(d => d.FuelSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_FuelSystems");

                entity.HasOne(d => d.Payload1)
                    .WithMany(p => p.BattlecruiserConfigurationPayload1s)
                    .HasForeignKey(d => d.Payload1Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_Payloads1");

                entity.HasOne(d => d.Payload2)
                    .WithMany(p => p.BattlecruiserConfigurationPayload2s)
                    .HasForeignKey(d => d.Payload2Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_Payloads2");

                entity.HasOne(d => d.PointDefenseSystem)
                    .WithMany(p => p.BattlecruiserConfigurations)
                    .HasForeignKey(d => d.PointDefenseSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_PointDefenseSystems");

                entity.HasOne(d => d.Thruster)
                    .WithMany(p => p.BattlecruiserConfigurations)
                    .HasForeignKey(d => d.ThrusterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_Thrusters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BattlecruiserConfigurations)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_Users");

                entity.HasOne(d => d.WeaponFrame1)
                    .WithMany(p => p.BattlecruiserConfigurationWeaponFrame1s)
                    .HasForeignKey(d => d.WeaponFrame1Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_WeaponFrames1");

                entity.HasOne(d => d.WeaponFrame2)
                    .WithMany(p => p.BattlecruiserConfigurationWeaponFrame2s)
                    .HasForeignKey(d => d.WeaponFrame2Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BattlecruiserConfigurations_WeaponFrames2");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.HasKey(e => e.CarrierGuid);

                entity.Property(e => e.CarrierGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.CarrierPointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carriers(PointDefenseSystems)_Carriers");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Carriers)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carriers_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Carriers)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carriers_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.CarrierWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carriers(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<CarrierConfiguration>(entity =>
            {
                entity.HasKey(e => e.CarrierConfigurationGuid);

                entity.Property(e => e.CarrierConfigurationGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Armor)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.ArmorGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_Armors");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.CarrierGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_Carriers");

                entity.HasOne(d => d.FuelSystem)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.FuelSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_FuelSystems");

                entity.HasOne(d => d.Payload)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.PayloadGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_Payloads");

                entity.HasOne(d => d.PointDefenseSystem)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.PointDefenseSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_PointDefenseSystems");

                entity.HasOne(d => d.Thruster)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.ThrusterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_Thrusters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_Users");

                entity.HasOne(d => d.WeaponFrame)
                    .WithMany(p => p.CarrierConfigurations)
                    .HasForeignKey(d => d.WeaponFrameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarrierConfigurations_WeaponFrames");
            });

            modelBuilder.Entity<Corvette>(entity =>
            {
                entity.HasKey(e => e.CorvetteGuid);

                entity.Property(e => e.CorvetteGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.CorvettePointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Corvettes(PointDefenseSystems)_PowerLevels");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Corvettes)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Corvettes_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Corvettes)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Corvettes_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.CorvetteWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Corvettes(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<CorvetteConfiguration>(entity =>
            {
                entity.HasKey(e => e.CorvetteConfigurationGuid);

                entity.Property(e => e.CorvetteConfigurationGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Armor)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.ArmorGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_Armors");

                entity.HasOne(d => d.Corvette)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.CorvetteGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_Corvettes");

                entity.HasOne(d => d.FuelSystem)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.FuelSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_FuelSystems");

                entity.HasOne(d => d.Payload)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.PayloadGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_Payloads");

                entity.HasOne(d => d.PointDefenseSystem)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.PointDefenseSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_PointDefenseSystems");

                entity.HasOne(d => d.Thruster)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.ThrusterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_Thrusters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_Users");

                entity.HasOne(d => d.WeaponFrame)
                    .WithMany(p => p.CorvetteConfigurations)
                    .HasForeignKey(d => d.WeaponFrameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorvetteConfigurations_WeaponFrames");
            });

            modelBuilder.Entity<Destroyer>(entity =>
            {
                entity.HasKey(e => e.DestroyerGuid);

                entity.Property(e => e.DestroyerGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.DestroyerPointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destroyers(PointDefenseSystems)_PowerLevels");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Destroyers)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destroyers_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Destroyers)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destroyers_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.DestroyerWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destroyers(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<DestroyerConfiguration>(entity =>
            {
                entity.HasKey(e => e.DestroyerConfigurationGuid);

                entity.Property(e => e.DestroyerConfigurationGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Armor)
                    .WithMany(p => p.DestroyerConfigurations)
                    .HasForeignKey(d => d.ArmorGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_Armors");

                entity.HasOne(d => d.Destroyer)
                    .WithMany(p => p.DestroyerConfigurations)
                    .HasForeignKey(d => d.DestroyerGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_Destroyers");

                entity.HasOne(d => d.FuelSystem)
                    .WithMany(p => p.DestroyerConfigurations)
                    .HasForeignKey(d => d.FuelSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_FuelSystems");

                entity.HasOne(d => d.Payload1)
                    .WithMany(p => p.DestroyerConfigurationPayload1s)
                    .HasForeignKey(d => d.Payload1Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_Payloads");

                entity.HasOne(d => d.Payload2)
                    .WithMany(p => p.DestroyerConfigurationPayload2s)
                    .HasForeignKey(d => d.Payload2Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_Payloads1");

                entity.HasOne(d => d.PointDefenseSystem)
                    .WithMany(p => p.DestroyerConfigurations)
                    .HasForeignKey(d => d.PointDefenseSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_PointDefenseSystems");

                entity.HasOne(d => d.Thruster)
                    .WithMany(p => p.DestroyerConfigurations)
                    .HasForeignKey(d => d.ThrusterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_Thrusters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DestroyerConfigurations)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_Users");

                entity.HasOne(d => d.WeaponFrame1)
                    .WithMany(p => p.DestroyerConfigurationWeaponFrame1s)
                    .HasForeignKey(d => d.WeaponFrame1Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_WeaponFrames");

                entity.HasOne(d => d.WeaponFrame2)
                    .WithMany(p => p.DestroyerConfigurationWeaponFrame2s)
                    .HasForeignKey(d => d.WeaponFrame2Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestroyerConfigurations_WeaponFrames1");
            });

            modelBuilder.Entity<Fighter>(entity =>
            {
                entity.HasKey(e => e.FighterGuid);

                entity.Property(e => e.FighterGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.FighterPointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighters(PointDefenseSystems)_PowerLevels");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Fighters)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighters_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Fighters)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighters_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.FighterWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fighters(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<FighterConfiguration>(entity =>
            {
                entity.HasKey(e => e.FighterConfigurationGuid);

                entity.Property(e => e.FighterConfigurationGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Armor)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.ArmorGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_Armors");

                entity.HasOne(d => d.Fighter)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.FighterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_Fighters");

                entity.HasOne(d => d.FuelSystem)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.FuelSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_FuelSystems");

                entity.HasOne(d => d.Payload)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.PayloadGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_Payloads");

                entity.HasOne(d => d.PointDefenseSystem)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.PointDefenseSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_PointDefenseSystems");

                entity.HasOne(d => d.Thruster)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.ThrusterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_Thrusters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_Users");

                entity.HasOne(d => d.WeaponFrame)
                    .WithMany(p => p.FighterConfigurations)
                    .HasForeignKey(d => d.WeaponFrameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FighterConfigurations_WeaponFrames");
            });

            modelBuilder.Entity<Fleet>(entity =>
            {
                entity.HasKey(e => e.FleetGuid);

                entity.Property(e => e.FleetGuid).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Fleets)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fleets_Users");
            });

            modelBuilder.Entity<FleetBattlecruiser>(entity =>
            {
                entity.HasKey(e => e.FleetBattlecruiserGuid)
                    .HasName("PK_FleetBattlecruisers_1");

                entity.Property(e => e.FleetBattlecruiserGuid).ValueGeneratedNever();

                entity.HasOne(d => d.BattlecruiserConfiguration)
                    .WithMany(p => p.FleetBattlecruisers)
                    .HasForeignKey(d => d.BattlecruiserConfigurationGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetBattlecruisers_BattlecruiserConfigurations");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.FleetBattlecruisers)
                    .HasForeignKey(d => d.FleetGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetBattlecruisers_Fleets1");
            });

            modelBuilder.Entity<FleetCarrier>(entity =>
            {
                entity.HasKey(e => e.FleetCarrierGuid);

                entity.Property(e => e.FleetCarrierGuid).ValueGeneratedNever();

                entity.HasOne(d => d.CarrierConfiguration)
                    .WithMany(p => p.FleetCarriers)
                    .HasForeignKey(d => d.CarrierConfigurationGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetCarriers_CarrierConfigurations");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.FleetCarriers)
                    .HasForeignKey(d => d.FleetGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetCarriers_Fleets");
            });

            modelBuilder.Entity<FleetCorvette>(entity =>
            {
                entity.HasKey(e => e.FleetCorvetteGuid);

                entity.Property(e => e.FleetCorvetteGuid).ValueGeneratedNever();

                entity.HasOne(d => d.CorvetteConfiguration)
                    .WithMany(p => p.FleetCorvettes)
                    .HasForeignKey(d => d.CorvetteConfigurationGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetCorvettes_CorvetteConfigurations");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.FleetCorvettes)
                    .HasForeignKey(d => d.FleetGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetCorvettes_Fleets");
            });

            modelBuilder.Entity<FleetDestroyer>(entity =>
            {
                entity.HasKey(e => e.FleetDestroyerGuid);

                entity.Property(e => e.FleetDestroyerGuid).ValueGeneratedNever();

                entity.HasOne(d => d.DestroyerConfiguration)
                    .WithMany(p => p.FleetDestroyers)
                    .HasForeignKey(d => d.DestroyerConfigurationGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetDestroyers_DestroyerConfigurations");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.FleetDestroyers)
                    .HasForeignKey(d => d.FleetGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetDestroyers_Fleets");
            });

            modelBuilder.Entity<FleetFighter>(entity =>
            {
                entity.HasKey(e => e.FleetFighterGuid);

                entity.Property(e => e.FleetFighterGuid).ValueGeneratedNever();

                entity.HasOne(d => d.FighterConfiguration)
                    .WithMany(p => p.FleetFighters)
                    .HasForeignKey(d => d.FighterConfigurationGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetFighters_FighterConfigurations");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.FleetFighters)
                    .HasForeignKey(d => d.FleetGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetFighters_Fleets");
            });

            modelBuilder.Entity<FleetFrigate>(entity =>
            {
                entity.HasKey(e => e.FleetFrigateGuid);

                entity.Property(e => e.FleetFrigateGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.FleetFrigates)
                    .HasForeignKey(d => d.FleetGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetFrigates_Fleets");

                entity.HasOne(d => d.FrigateConfiguration)
                    .WithMany(p => p.FleetFrigates)
                    .HasForeignKey(d => d.FrigateConfigurationGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FleetFrigates_FrigateConfigurations");
            });

            modelBuilder.Entity<Frigate>(entity =>
            {
                entity.HasKey(e => e.FrigateGuid);

                entity.Property(e => e.FrigateGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.FrigatePointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Frigates(PointDefenseSystems)_PowerLevels");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Frigates)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Frigates_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Frigates)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Frigates_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.FrigateWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Frigates(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<FrigateConfiguration>(entity =>
            {
                entity.HasKey(e => e.FrigateConfigurationGuid);

                entity.Property(e => e.FrigateConfigurationGuid).ValueGeneratedNever();

                entity.HasOne(d => d.Armor)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.ArmorGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_Armors");

                entity.HasOne(d => d.Frigate)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.FrigateGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_Frigates");

                entity.HasOne(d => d.FuelSystem)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.FuelSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_FuelSystems");

                entity.HasOne(d => d.Payload)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.PayloadGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_Payloads");

                entity.HasOne(d => d.PointDefenseSystem)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.PointDefenseSystemGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_PointDefenseSystems");

                entity.HasOne(d => d.Thruster)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.ThrusterGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_Thrusters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_Users");

                entity.HasOne(d => d.WeaponFrame)
                    .WithMany(p => p.FrigateConfigurations)
                    .HasForeignKey(d => d.WeaponFrameGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrigateConfigurations_WeaponFrames");
            });

            modelBuilder.Entity<FuelSystem>(entity =>
            {
                entity.HasKey(e => e.FuelSystemGuid);

                entity.Property(e => e.FuelSystemGuid).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.FuelSystems)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FuelSystems_SpaceshipTypes");
            });

            modelBuilder.Entity<Payload>(entity =>
            {
                entity.HasKey(e => e.PayloadGuid);

                entity.Property(e => e.PayloadGuid).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PayloadType)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.PayloadTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payloads_PayloadTypes");

                entity.HasOne(d => d.PowerLevel)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.PowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payloads_PowerLevels");

                entity.HasOne(d => d.WeaponFrameType)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.WeaponFrameTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payloads_WeaponFrameTypes");
            });

            modelBuilder.Entity<PayloadType>(entity =>
            {
                entity.HasKey(e => e.PayloadTypeGuid);

                entity.Property(e => e.PayloadTypeGuid).ValueGeneratedNever();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PointDefenseSystem>(entity =>
            {
                entity.HasKey(e => e.PointDefenseSystemGuid);

                entity.Property(e => e.PointDefenseSystemGuid).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PowerLevel)
                    .WithMany(p => p.PointDefenseSystems)
                    .HasForeignKey(d => d.PowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PointDefenseSystems_PowerLevels");
            });

            modelBuilder.Entity<PowerLevel>(entity =>
            {
                entity.HasKey(e => e.PowerLevelGuid)
                    .HasName("PK_WeaponPowerLevelTypes");

                entity.Property(e => e.PowerLevelGuid).ValueGeneratedNever();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Spaceship>(entity =>
            {
                entity.HasKey(e => e.SpaceshipGuid);

                entity.Property(e => e.SpaceshipGuid).ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.PointDefenseSystemPowerLevel)
                    .WithMany(p => p.SpaceshipPointDefenseSystemPowerLevels)
                    .HasForeignKey(d => d.PointDefenseSystemPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spaceships(PointDefenseSystems)_PowerLevels");

                entity.HasOne(d => d.SpaceshipCategory)
                    .WithMany(p => p.Spaceships)
                    .HasForeignKey(d => d.SpaceshipCategoryGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spaceships_SpaceshipCategories");

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Spaceships)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spaceships_SpaceshipTypes");

                entity.HasOne(d => d.WeaponPowerLevel)
                    .WithMany(p => p.SpaceshipWeaponPowerLevels)
                    .HasForeignKey(d => d.WeaponPowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spaceships(Weapons)_PowerLevels");
            });

            modelBuilder.Entity<SpaceshipCategory>(entity =>
            {
                entity.HasKey(e => e.SpaceshipCategoryGuid);

                entity.Property(e => e.SpaceshipCategoryGuid).ValueGeneratedNever();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SpaceshipType>(entity =>
            {
                entity.HasKey(e => e.SpaceshipTypeGuid);

                entity.Property(e => e.SpaceshipTypeGuid).ValueGeneratedNever();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Thruster>(entity =>
            {
                entity.HasKey(e => e.ThrusterGuid);

                entity.Property(e => e.ThrusterGuid).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Thrusters)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Thrusters_SpaceshipTypes");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserGuid);

                entity.Property(e => e.UserGuid).ValueGeneratedNever();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<WeaponFrame>(entity =>
            {
                entity.HasKey(e => e.WeaponFrameGuid);

                entity.Property(e => e.WeaponFrameGuid).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PowerLevel)
                    .WithMany(p => p.WeaponFrames)
                    .HasForeignKey(d => d.PowerLevelGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeaponFrames_PowerLevels");

                entity.HasOne(d => d.WeaponFrameType)
                    .WithMany(p => p.WeaponFrames)
                    .HasForeignKey(d => d.WeaponFrameTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeaponFrames_WeaponFrameTypes");
            });

            modelBuilder.Entity<WeaponFrameType>(entity =>
            {
                entity.HasKey(e => e.WeaponFrameTypeGuid);

                entity.Property(e => e.WeaponFrameTypeGuid).ValueGeneratedNever();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
