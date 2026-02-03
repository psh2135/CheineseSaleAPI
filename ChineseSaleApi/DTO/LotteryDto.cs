namespace ChineseSaleApi.DTO
{
    public class RunLotteryDto
    {
        public int GiftId { get; set; }
    }
    public class LotteryResultDto
    {
        public int GiftId { get; set; }
        public string GiftTitle { get; set; } = string.Empty;
        public int WinningTicketId { get; set; }
        public int WinnerUserId { get; set; }
        public string WinnerName { get; set; } = string.Empty; // שם מלא של הזוכה
    }

}
