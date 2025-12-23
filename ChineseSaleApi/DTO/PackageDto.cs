using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    public class CreatePackageDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(20, 9000)]
        public decimal Price { get; set; }
        [Required]
        [Range(20, 9000)]
        public int TicketsCount { get; set; }
    }
    public class PackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int TicketsCount { get; set; }
    }

}
