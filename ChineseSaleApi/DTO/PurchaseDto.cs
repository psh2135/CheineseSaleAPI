namespace ChineseSaleApi.DTO
{
    public class CreatePurchaseDto
    {
        public int BuyerId { get; set; }
        public int PackageId { get; set; }
        public int Quantity { get; set; }
    }
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchasedAt { get; set; }
    }

}
