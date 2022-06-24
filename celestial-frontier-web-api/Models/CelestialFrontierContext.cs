using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace celestial_frontier_web_api.Models
{
    public partial class CelestialFrontierContext : DbContext
    {
        public CelestialFrontierContext()
        {
        }

        public CelestialFrontierContext(DbContextOptions<CelestialFrontierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Armor> Armors { get; set; } = null!;
        public virtual DbSet<FuelSystem> FuelSystems { get; set; } = null!;
        public virtual DbSet<Payload> Payloads { get; set; } = null!;
        public virtual DbSet<PayloadType> PayloadTypes { get; set; } = null!;
        public virtual DbSet<PointDefenseSystem> PointDefenseSystems { get; set; } = null!;
        public virtual DbSet<PowerLevel> PowerLevels { get; set; } = null!;
        public virtual DbSet<Spaceship> Spaceships { get; set; } = null!;
        public virtual DbSet<SpaceshipCategory> SpaceshipCategories { get; set; } = null!;
        public virtual DbSet<SpaceshipType> SpaceshipTypes { get; set; } = null!;
        public virtual DbSet<Thruster> Thrusters { get; set; } = null!;
        public virtual DbSet<WeaponFrame> WeaponFrames { get; set; } = null!;
        public virtual DbSet<WeaponFrameType> WeaponFrameTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=AARONS-DESKTOP\\SQLEXPRESS;Initial Catalog=CelestialFrontier;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Armor>(entity =>
            {
                entity.HasKey(e => e.ArmorGuid);

                entity.Property(e => e.ArmorGuid).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Armors)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Armors_SpaceshipTypes");
            });

            modelBuilder.Entity<FuelSystem>(entity =>
            {
                entity.HasKey(e => e.FuelSystemGuid);

                entity.Property(e => e.FuelSystemGuid).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(100);

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

                entity.Property(e => e.Title).HasMaxLength(100);

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

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            modelBuilder.Entity<PointDefenseSystem>(entity =>
            {
                entity.HasKey(e => e.PointDefenseSystemGuid);

                entity.Property(e => e.PointDefenseSystemGuid).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(100);

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

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            modelBuilder.Entity<Spaceship>(entity =>
            {
                entity.HasKey(e => e.SpaceshipGuid);

                entity.Property(e => e.SpaceshipGuid).ValueGeneratedNever();

                entity.Property(e => e.Class).HasMaxLength(100);

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

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            modelBuilder.Entity<SpaceshipType>(entity =>
            {
                entity.HasKey(e => e.SpaceshipTypeGuid);

                entity.Property(e => e.SpaceshipTypeGuid).ValueGeneratedNever();

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            modelBuilder.Entity<Thruster>(entity =>
            {
                entity.HasKey(e => e.ThrusterGuid);

                entity.Property(e => e.ThrusterGuid).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.SpaceshipType)
                    .WithMany(p => p.Thrusters)
                    .HasForeignKey(d => d.SpaceshipTypeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Thrusters_SpaceshipTypes");
            });

            modelBuilder.Entity<WeaponFrame>(entity =>
            {
                entity.HasKey(e => e.WeaponFrameGuid);

                entity.Property(e => e.WeaponFrameGuid).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(100);

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

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
