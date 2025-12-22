namespace ChineseSaleApi.DTO
{
    public class RunLotteryDto
    {
        public int GiftId { get; set; }
    }
    public class LotteryResultDto
    {
        public int GiftId { get; set; }
        public int WinnerUserId { get; set; }
        public int WinningTicketId { get; set; }
    }

}
