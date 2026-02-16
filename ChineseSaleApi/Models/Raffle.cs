namespace ChineseSaleApi.Models
{
    public class Raffle
    {
        public int Id { get; set; }

        public DateTime OpeningDate { get; set; }

        public bool IsLocked => DateTime.UtcNow >= OpeningDate;
    }

}
