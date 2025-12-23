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
            base.OnModelCreating(modelBuilder);

            // =========================
            // User
            // =========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.UserName)
                      .HasMaxLength(50); // ❌ לא ייחודי

                entity.Property(u => u.PasswordHash)
                      .HasMaxLength(256)
                      .IsRequired();

                entity.Property(u => u.Phone)
                      .HasMaxLength(20);

                entity.Property(u => u.Role)
                      .HasMaxLength(20)
                      .HasDefaultValue("Buyer");

                entity.Property(u => u.FirstName)
                      .HasMaxLength(50);

                entity.Property(u => u.LastName)
                      .HasMaxLength(50);

                entity.Property(u => u.Address)
                      .HasMaxLength(200);

                // ✅ אימייל ייחודי
                entity.HasIndex(u => u.Email)
                      .IsUnique();
            });

            // =========================
            // Gift -> Donor
            // =========================
            modelBuilder.Entity<Gift>()
                .Property(g => g.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Gift>()
                .Property(g => g.Description)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Donor)
                .WithMany(u => u.Gifts)
                .HasForeignKey(g => g.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Purchase
            // =========================
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Buyer)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Package)
                .WithMany(pkg => pkg.Purchases)
                .HasForeignKey(p => p.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Package
            // =========================
            modelBuilder.Entity<Package>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Package>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            // =========================
            // Ticket
            // =========================
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Purchase)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Gift)
                .WithMany(g => g.Tickets)
                .HasForeignKey(t => t.GiftId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // LotteryResult
            // =========================
            modelBuilder.Entity<LotteryResult>()
                .HasOne(l => l.Gift)
                .WithOne()
                .HasForeignKey<LotteryResult>(l => l.GiftId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotteryResult>()
                .HasOne(l => l.Winner)
                .WithMany()
                .HasForeignKey(l => l.WinnerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        using Microsoft.EntityFrameworkCore;

namespace ChineseSaleApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Gift> Gifts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.UserName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .HasMaxLength(200)
                      .IsRequired()
                      .HasDefaultValue("Buyer");

                // הוספת ולידציה לטלפון
                entity.Property(u => u.Phone)
                      .HasMaxLength(200)
                      .HasAnnotation("RegularExpression", @"^\+?[0-9\s\-]{7,15}$"); // רק ספרות, + ורווחים

                entity.Property(u => u.PasswordHash)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(u => u.LastName)
                      .HasMaxLength(200);

                entity.Property(u => u.FirstName)
                      .HasMaxLength(200);

                // הוספת ולידציה לאימייל
                entity.Property(u => u.Email)
                      .HasMaxLength(200)
                      .IsRequired()
                      .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                entity.Property(u => u.Address)
                      .HasMaxLength(200);

                entity.HasIndex(u => u.Email)
                      .IsUnique();
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(p => p.Name)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(p => p.Price)
                      .HasPrecision(10, 2);
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.Property(g => g.Title)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(g => g.Description)
                      .HasMaxLength(200)
                      .IsRequired();
            });
        }
    }
}

    }
}
