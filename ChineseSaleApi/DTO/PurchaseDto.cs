using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    public class CreatePurchaseDto
    {
        [Required]
        public int BuyerId { get; set; }
        [Required]
        public int PackageId { get; set; }
        [Required]
        [Range(1, 100)]
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
