using ChineseSaleApi.Models;

namespace ChineseSaleApi.Models
{
    public class Gift
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int DonorId { get; set; }
        public User Donor { get; set; } = null!;

        // הגרלה
        public bool IsDrawn { get; set; } = false;
        public int? WinnerUserId { get; set; }
        public User? Winner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
