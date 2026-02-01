using Microsoft.EntityFrameworkCore;
using ChineseSaleApi.Models;

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

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.UserName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.PasswordHash)
                      .HasMaxLength(256)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .HasMaxLength(20)
                      .IsRequired()
                      .HasDefaultValue("Buyer");

                entity.HasIndex(u => u.Email)
                      .IsUnique();
            });

            // Gift
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.Property(g => g.Title)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(g => g.Description)
                      .HasMaxLength(500)
                      .IsRequired();

                entity.HasOne(g => g.Donor)
                      .WithMany(u => u.Gifts)
                      .HasForeignKey(g => g.DonorId)
                      .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(g => g.Category)
                //     .WithMany(u => u.Gifts)
                //     .HasForeignKey(g => g.CategotyId)
                //     .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Winner)
                      .WithMany()
                      .HasForeignKey(g => g.WinnerUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Package
            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(p => p.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(p => p.Price)
                      .HasPrecision(10, 2);

                entity.Property(p => p.TicketsCount)
                      .IsRequired();
            });

            // Purchase
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasOne(p => p.Buyer)
                      .WithMany(u => u.Purchases)
                      .HasForeignKey(p => p.BuyerId)
                      .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(p => p.Package)
                //      .WithMany(pkg => pkg.Purchases)
                //      .HasForeignKey(p => p.PackageId)
                //      .OnDelete(DeleteBehavior.Restrict);
            });

            // Ticket
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
            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name)
                      .HasMaxLength(50)
                      .IsRequired();
            });
        }
    }
}
//        using Microsoft.EntityFrameworkCore;

//namespace ChineseSaleApi.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public DbSet<User> Users { get; set; }
//        public DbSet<Package> Packages { get; set; }
//        public DbSet<Gift> Gifts { get; set; }

//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.Property(u => u.UserName)
//                      .HasMaxLength(200)
//                      .IsRequired();

//                entity.Property(u => u.Role)
//                      .HasMaxLength(200)
//                      .IsRequired()
//                      .HasDefaultValue("Buyer");

//                // הוספת ולידציה לטלפון
//                entity.Property(u => u.Phone)
//                      .HasMaxLength(200)
//                      .HasAnnotation("RegularExpression", @"^\+?[0-9\s\-]{7,15}$"); // רק ספרות, + ורווחים

//                entity.Property(u => u.PasswordHash)
//                      .HasMaxLength(200)
//                      .IsRequired();

//                entity.Property(u => u.LastName)
//                      .HasMaxLength(200);

//                entity.Property(u => u.FirstName)
//                      .HasMaxLength(200);

//                // הוספת ולידציה לאימייל
//                entity.Property(u => u.Email)
//                      .HasMaxLength(200)
//                      .IsRequired()
//                      .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

//                entity.Property(u => u.Address)
//                      .HasMaxLength(200);

//                entity.HasIndex(u => u.Email)
//                      .IsUnique();
//            });

//            modelBuilder.Entity<Package>(entity =>
//            {
//                entity.Property(p => p.Name)
//                      .HasMaxLength(200)
//                      .IsRequired();

//                entity.Property(p => p.Price)
//                      .HasPrecision(10, 2);
//            });

//            modelBuilder.Entity<Gift>(entity =>
//            {
//                entity.Property(g => g.Title)
//                      .HasMaxLength(200)
//                      .IsRequired();

//                entity.Property(g => g.Description)
//                      .HasMaxLength(200)
//                      .IsRequired();
//            });
//        }
//    }
//}

//    }
//}
