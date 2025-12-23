namespace ChineseSaleApi.DTO
{
    using System.ComponentModel.DataAnnotations;

    namespace ChineseSaleApi.DTO
    {
        // DTO בסיסי לתצוגה
        public class TicketDto
        {
            public int Id { get; set; }
            public int GiftId { get; set; }
        }

        // DTO להצגת כרטיסי רוכש
        // GET /buyers/{id}/tickets
        public class BuyerTicketsDto
        {
            public int BuyerId { get; set; }
            public List<TicketDto> Tickets { get; set; } = new();
        }

        // DTO לניהול / אדמין
        // GET /admin/tickets
        public class TicketAdminDto
        {
            public int TicketId { get; set; }
            public int GiftId { get; set; }
            public int BuyerId { get; set; }
            public int PurchaseId { get; set; }
            public DateTime CreatedAt { get; set; }
        }


        public class CreateTicketDto
        {
            [Required]
            public int PurchaseId { get; set; }

            [Required]
            public int BuyerId { get; set; }

            [Required]
            public int GiftId { get; set; }
        }
    }


}
