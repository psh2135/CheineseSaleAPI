//using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
//using ChineseSaleApi.Models;
//using System.ComponentModel.DataAnnotations;

//namespace ChineseSaleApi.DTO
//{
//    public class CreatePurchaseDto
//    {
//        [Required]
//        public int BuyerId { get; set; }

//    }
//    public class AddToCartDto
//    {
//        [Required]
//        public int GiftId { get; set; }
//    }
//    public class PurchaseDto
//    {
//        public int Id { get; set; }
//        public string Status { get; set; } = string.Empty;
//        public DateTime CreatedAt { get; set; }
//        public decimal TotalPrice { get; set; }
//        public List<TicketDto> Tickets { get; set; } = new();
//    }
//    public class AdminDashboardDto
//    {
//        public decimal TotalRevenue { get; set; }
//        public int TotalTicketsSold { get; set; }
//        public int TotalParticipants { get; set;}
//    }
//}
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
        [Required]
        public int GiftId { get; set; }
    }
    public class CartDto
    {
        public List<int> GiftsAtCart { get; set; } = new List<int>();
    }
    public class PurchaseDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public List<TicketDto> Tickets { get; set; } = new();
    }
    public class AdminDashboardDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalTicketsSold { get; set; }
        public int TotalParticipants { get; set; }
    }
}
