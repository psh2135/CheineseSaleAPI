namespace ChineseSaleApi.models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public User Buyer { get; set; } = null!;
        public int PackageId { get; set; }
        public Package Package { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
