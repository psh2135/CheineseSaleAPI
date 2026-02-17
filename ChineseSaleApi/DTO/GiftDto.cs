//using ChineseSaleApi.Models;
//using System.ComponentModel.DataAnnotations;

//namespace ChineseSaleApi.DTO
//{
//    public class CreateGiftDto
//    {
//        [Required]
//        [StringLength(100, MinimumLength = 2)]
//        public string Title { get; set; } = null!;
//        [Required]
//        [StringLength(500, MinimumLength = 2)]
//        public string Description { get; set; } = null!;
//        [Required]
//        public string ImageUrl { get; set; }
//        [Required]
//        public decimal Price { get; set; }
//        [Required]
//        public int DonorId { get; set; }
//        [Required]
//        public List<int> CategoryId { get; set; }
//    }
//    public class GiftDto
//    {
//        public int Id { get; set; }
//        public string Title { get; set; } = null!;
//        public string Description { get; set; } = null!;
//        public string ImageUrl { get; set; }
//        public decimal Price { get; set; } = 0;
//        public bool IsDrawn { get; set; } = false;

//        public List<Category> Categories { get; set; } = null!;

//    }

//}
using ChineseSaleApi.DTO.ChineseSaleApi.DTOs;
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
        public List<int> CategoryIds { get; set; }
    }
    public class GiftDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; }
        public decimal Price { get; set; } = 0;
        public bool IsDrawn { get; set; } = false;
        public List<CategoryDto> Categories { get; set; }

    }
    public class UpdateGiftDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public int? DonorId { get; set; }

        public List<int>? CategoryIds { get; set; }
    }
    public class WinnerGiftDto
    {
        public int GiftId { get; set; }
        public string GiftTitle { get; set; } = null!;
        public decimal Price { get; set; }

        public int WinnerId { get; set; }
        public string WinnerName { get; set; } = null!;
        public string Email { get; set; } = null;
    }

}
