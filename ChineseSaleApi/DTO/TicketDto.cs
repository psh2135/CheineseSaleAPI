namespace ChineseSaleApi.DTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
    }
    public class BuyerTicketsDto
    {
        public int BuyerId { get; set; }
        public List<TicketDto> Tickets { get; set; } = new();
    }

}
