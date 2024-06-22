using Microsoft.EntityFrameworkCore;
using RoyalParking.Core.Entities;

namespace RoyalParking.API.DataContext;

public partial class RoyalParkingContext : DbContext
{
    public RoyalParkingContext()
    {
    }

    public RoyalParkingContext(DbContextOptions<RoyalParkingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<ParkingSlip> ParkingSlips { get; set; }

    public virtual DbSet<ParkingSpot> ParkingSpots { get; set; }

    public virtual DbSet<ParkingStatus> ParkingStatuses { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Valet> Valets { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.UserId, "UC_Customer_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_User");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discount");

            entity.HasIndex(e => e.Type, "UC_Discount_Type").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Multiplier).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParkingSlip>(entity =>
        {
            entity.ToTable("ParkingSlip");

            entity.Property(e => e.AmountDue).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.SavedDetails)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Discount).WithMany(p => p.ParkingSlips)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_ParkingSlip_Discount");

            entity.HasOne(d => d.ParkingSpot).WithMany(p => p.ParkingSlips)
                .HasForeignKey(d => d.ParkingSpotId)
                .HasConstraintName("FK_ParkingSlip_ParkingSpot");

            entity.HasOne(d => d.ParkingStatus).WithMany(p => p.ParkingSlips)
                .HasForeignKey(d => d.ParkingStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkingSlip_ParkingStatus");

            entity.HasOne(d => d.Rate).WithMany(p => p.ParkingSlips)
                .HasForeignKey(d => d.RateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkingSlip_Rate");

            entity.HasOne(d => d.Valet).WithMany(p => p.ParkingSlips)
                .HasForeignKey(d => d.ValetId)
                .HasConstraintName("FK_ParkingSlip_Valet");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.ParkingSlips)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkingSlip_Vehicle");
        });

        modelBuilder.Entity<ParkingSpot>(entity =>
        {
            entity.ToTable("ParkingSpot");

            entity.HasIndex(e => e.Number, "UC_ParkingSpot_Number").IsUnique();

            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParkingStatus>(entity =>
        {
            entity.ToTable("ParkingStatus");

            entity.HasIndex(e => e.Status, "UC_ParkingStatus_Status").IsUnique();

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.ToTable("Rate");

            entity.Property(e => e.SaturdaySundayRate).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.SpecialEventRate).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.WeekdayRate).HasColumnType("decimal(13, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UC_User_Email").IsUnique();

            entity.HasIndex(e => e.Phone, "UC_User_Phone").IsUnique();

            entity.HasIndex(e => e.Username, "UC_User_Username").IsUnique();

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salt)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Valet>(entity =>
        {
            entity.ToTable("Valet");

            entity.HasIndex(e => e.UserId, "UC_Valet_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.Valet)
                .HasForeignKey<Valet>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Valet_User");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicle");

            entity.HasIndex(e => new { e.LicensePlate, e.StateLicensedIn }, "UC_Vehicle_License").IsUnique();

            entity.Property(e => e.Color)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Make)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StateLicensedIn)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
