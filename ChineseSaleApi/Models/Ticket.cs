namespace ChineseSaleApi.models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int GiftId { get; set; }           
        public Gift Gift { get; set; } = null!;
        public int BuyerId { get; set; }          
        public User Buyer { get; set; } = null!;
        public int PurchaseId { get; set; }  
        public Purchase Purchase { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
