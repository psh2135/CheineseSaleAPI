using Microsoft.EntityFrameworkCore;
using ChineseSaleApi.Models;
using System.Text.Json;

namespace ChineseSaleApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- User ---
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.UserName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
                entity.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
                entity.Property(u => u.Role).HasMaxLength(20).IsRequired().HasDefaultValue("Buyer");
                entity.HasIndex(u => u.Email).IsUnique();
            });

            // --- Gift ---
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.Property(g => g.Title).HasMaxLength(100).IsRequired();
                entity.Property(g => g.Description).HasMaxLength(500).IsRequired();

                entity.HasOne(g => g.Donor)
                      .WithMany(u => u.Gifts)
                      .HasForeignKey(g => g.DonorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Winner)
                      .WithMany()
                      .HasForeignKey(g => g.WinnerUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(g => g.Categories)
                      .WithMany(c => c.Gifts)
                      .UsingEntity(j => j.ToTable("CategoryGifts"));
            });

            // --- Package ---
            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Price).HasPrecision(10, 2);
                entity.Property(p => p.TicketsCount).IsRequired();
            });

            // --- Purchase ---
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasOne(p => p.Buyer)
                      .WithMany(u => u.Purchases)
                      .HasForeignKey(p => p.BuyerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.GiftsAtCart)
                      .HasConversion(
                          v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                          v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)null) ?? new List<int>()
                      );
            });

            // --- Ticket ---
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasOne(t => t.Purchase)
                      .WithMany(p => p.Tickets)
                      .HasForeignKey(t => t.PurchaseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Gift)
                      .WithMany(g => g.Tickets)
                      .HasForeignKey(t => t.GiftId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // --- Category ---
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(50).IsRequired();
            });
        }
    }
}