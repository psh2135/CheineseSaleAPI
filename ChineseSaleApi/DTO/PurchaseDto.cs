using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    public class CreatePurchaseDto
    {
        [Required]
        public int BuyerId { get; set; }
        
    }
    public class AddToCartDto
    {
        public int BuyerId { get; set; }
        public int GiftId { get; set; }
    }
    public class PurchaseDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<TicketDto> Tickets { get; set; } = new();
    }
    
}
