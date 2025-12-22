using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace ChineseSaleApi.models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [StringLength(50)]
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        [StringLength(20)]
        public string? Phone { get; set; }
        public string Role { get; set; } = "Buyer"; // Admin / Buyer / Donor
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(50)]
        public string? Address { get; set; }

        public ICollection<Gift> Gifts { get; set; } = new List<Gift>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
