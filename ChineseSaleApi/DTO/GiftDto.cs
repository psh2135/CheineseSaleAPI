using ChineseSaleApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    public class CreateGiftDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int DonorId { get; set; }
        [Required]
        public List<int> CategoryId { get; set; }
    }
    public class GiftDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; }
        public decimal Price { get; set; } = 0;
        public bool IsDrawn { get; set; } = false;

        public List<Category> Categories { get; set; } = null!;
        
    }

}
