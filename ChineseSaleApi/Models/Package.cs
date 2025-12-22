namespace ChineseSaleApi.models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int TicketsCount { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Purchase>? Purchases { get; set; }
    }
}
