namespace ChineseSaleApi.DTO
{
    public class CreateGiftDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DonorId { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class GiftDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

}
