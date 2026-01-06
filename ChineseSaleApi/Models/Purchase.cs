using ChineseSaleApi.Models;

namespace ChineseSaleApi.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }
        public User Buyer { get; set; } = null!;

        public int PackageId { get; set; }
        public Package Package { get; set; } = null!;

        public int Quantity { get; set; }

        public PurchaseStatus Status { get; set; } = PurchaseStatus.Completed;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    public enum PurchaseStatus
    {
        Draft = 0,
        Completed = 1,
        Cancelled = 2
    }
}
