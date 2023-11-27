using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Models;

public partial class FoodOrderingDbContext : DbContext
{
    public FoodOrderingDbContext()
    {
    }

    public FoodOrderingDbContext(DbContextOptions<FoodOrderingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addon> Addons { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Food> Foods { get; set; }
    public virtual DbSet<Timing> Timings { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Rider> Riders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<RestaurantSection> RestaurantSections { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.AddonId).HasName("PK__Addons__742895337DF409D9");

            entity.Property(e => e.AddonId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime"); 
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Option)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.Food).WithMany(p => p.Addons)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK_Addons_Foods");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admin");
            entity.HasKey(e => e.AdminId).HasName("PK_Admin");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.ProfileImage).HasColumnType("image");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__Foods__856DB3EB2DB7087B");

            entity.Property(e => e.FoodId).ValueGeneratedNever();
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.VariationsTitle)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Foods)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK_Foods_Restaurants");
        });
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categories__856DB3EB2DB7087B");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");            
            entity.Property(e => e.CategoryName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantId).ValueGeneratedNever();
            entity.Property(e => e.FoodId).ValueGeneratedNever();
        });
        modelBuilder.Entity<Timing>(entity =>
        {
            entity.HasKey(e => e.TimingId).HasName("PK__Categories__856DB3EB2DB7087B");

            entity.Property(e => e.TimingId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Day)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OpenTiming)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CloseTiming)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantId).ValueGeneratedNever();            
        });
        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Options__92C7A1FFC3AE2B36");

            entity.Property(e => e.OptionId).ValueGeneratedNever(); 
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.Addon).WithMany(p => p.Options)
                .HasForeignKey(d => d.AddonId)
                .HasConstraintName("FK_Options_Addons");
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Options__92C7A1FFC3AE2B36");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDateTime).HasColumnType("datetime");
            entity.Property(e => e.Items)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymantMode)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantId).ValueGeneratedNever();
        });
        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__87454C95F5B67EFC");

            entity.Property(e => e.RestaurantId).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Available)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderPrifix)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_Restaurants_Vendors");
        });

        modelBuilder.Entity<Rider>(entity =>
        {
            entity.HasKey(e => e.RiderId).HasName("PK__Riders__7D726C60242EE683");

            entity.Property(e => e.RiderId).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Available)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OfficeAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OtherAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendors__FC8618F334BB5A43");

            entity.Property(e => e.VendorId).ValueGeneratedNever();            
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);           
        });
        modelBuilder.Entity<RestaurantSection>(entity =>
        {
            entity.HasKey(e => e.RestaurantSectionId).HasName("PK__RestaurantSections__FC8618F334BB5A43");

            entity.Property(e => e.RestaurantSectionId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Restaurants)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
