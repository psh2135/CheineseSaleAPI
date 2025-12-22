namespace ChineseSaleApi.models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DonorId { get; set; }           
        public User Donor { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
