using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    namespace ChineseSaleApi.DTOs
    {
        public class CategoryDto
        {

            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class CreateCategoryDto
        {
            [Required]
            [StringLength(100, MinimumLength = 2)]
            public string Name { get; set; }
        }
    }
}
