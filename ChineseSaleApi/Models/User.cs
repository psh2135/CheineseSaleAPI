namespace ChineseSaleApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Buyer; // Admin / Buyer / Donor
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Gift> Gifts { get; set; } = new List<Gift>();//donor
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();//buyer
    }
    public enum UserRole
    {
        Buyer,
        Donor,
        Admin
    }
}
