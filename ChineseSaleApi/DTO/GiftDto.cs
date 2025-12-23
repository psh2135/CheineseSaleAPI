using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    public class CreateGiftDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Description { get; set; } = null!;
        [Required]
        public int DonorId { get; set; }
    }
    public class GiftDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

}
