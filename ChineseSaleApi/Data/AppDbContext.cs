using Microsoft.EntityFrameworkCore;
using ChineseSaleApi.models;

namespace ChineseSaleApi.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<LotteryResult> LotteryResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gift -> Donor (User)
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Donor)
                .WithMany(u => u.Gifts)
                .HasForeignKey(g => g.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Purchase -> Buyer (User)
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Buyer)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Purchase -> Package
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Package)
                .WithMany(pkg => pkg.Purchases)
                .HasForeignKey(p => p.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> Purchase
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Purchase)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket -> Gift
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Gift)
                .WithMany(g => g.Tickets)
                .HasForeignKey(t => t.GiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> Buyer
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // LotteryResult -> Gift (One to One)
            modelBuilder.Entity<LotteryResult>()
                .HasOne(l => l.Gift)
                .WithOne()
                .HasForeignKey<LotteryResult>(l => l.GiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // LotteryResult -> Winner (User)
            modelBuilder.Entity<LotteryResult>()
                .HasOne(l => l.Winner)
                .WithMany()
                .HasForeignKey(l => l.WinnerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
