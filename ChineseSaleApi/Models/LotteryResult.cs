namespace ChineseSaleApi.models
{
    public class LotteryResult
    {
        public int Id { get; set; }

        public int GiftId { get; set; }
        public Gift Gift { get; set; } = null!;

        public int WinnerUserId { get; set; }
        public User Winner { get; set; } = null!;

        public int WinningTicketId { get; set; }
        public Ticket WinningTicket { get; set; } = null!;

        public int DrawnByAdminId { get; set; }
        public User DrawnByAdmin { get; set; } = null!;

        public DateTime DrawnAt { get; set; } = DateTime.UtcNow;
    }
}
