using ChineseSaleApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ChineseSaleApi.DTO
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Buyer;
        public string Token { get; set; } = null!;
    }
    public class CreateDonorDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }

}
