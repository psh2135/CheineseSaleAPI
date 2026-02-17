using AutoMapper;
using BCrypt.Net;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using ChineseSaleApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChineseSaleApi.Services
{
    public interface IUserService
    {
        UserDto Register(CreateUserDto dto);
        UserDto AddDonor(CreateUserDto dto);
        UserDto Login(string email, string password);
        IEnumerable<UserDto> GetAllDonors();
        Task<IEnumerable<UserDto>> GetAllDonorsAsync();

        Task<UserDto?> UpdateDonorAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteDonorAsync(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IRaffleStateService _stateService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(IUserRepository repo, IMapper mapper, IConfiguration config, IRaffleStateService stateService)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
            _stateService = stateService;
        }

        public UserDto Register(CreateUserDto dto) => CreateUserWithRole(dto, UserRole.Buyer);

        public UserDto AddDonor(CreateUserDto dto)
        {

            if (_stateService.IsRaffleLocked())
                throw new InvalidOperationException("Raffle is locked");

            return CreateUserWithRole(dto, UserRole.Donor);
        }


        private UserDto CreateUserWithRole(CreateUserDto dto, UserRole role)
        {
            // ✅ בדיקה אם המייל כבר קיים
            var existingUser = _repo.GetAll().FirstOrDefault(u => u.Email == dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("המייל כבר רשום במערכת");
            }

            // ✅ בדיקה אם שם המשתמש כבר קיים
            var existingUserName = _repo.GetAll().FirstOrDefault(u => u.UserName == dto.UserName);
            if (existingUserName != null)
            {
                throw new InvalidOperationException("שם המשתמש כבר קיים במערכת");
            }

            // ✅ בדיקת תקינות מייל
            if (!IsValidEmail(dto.Email))
            {
                throw new InvalidOperationException("כתובת האימייל לא תקינה");
            }

            // ✅ בדיקת אורך סיסמה
            if (dto.Password.Length < 6)
            {
                throw new InvalidOperationException("הסיסמה חייבת להכיל לפחות 6 תווים");
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = role;

            _repo.Add(user);
            _repo.Save();

            return _mapper.Map<UserDto>(user);
        }

        // ✅ פונקציית עזר לולידציית מייל
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public UserDto Login(string email, string password)
        {
            var user = _repo.GetByEmail(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            var userDto = _mapper.Map<UserDto>(user);

            userDto.Token = GenerateJwtToken(user);

            return userDto;
        }

        public IEnumerable<UserDto> GetAllDonors()
        {
            var donors = _repo.GetUsersByRole(UserRole.Donor);

            return _mapper.Map<IEnumerable<UserDto>>(donors);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IEnumerable<UserDto>> GetAllDonorsAsync()
        {
            var donors = await _repo.GetUsersByRoleAsync(UserRole.Donor);
            return _mapper.Map<IEnumerable<UserDto>>(donors);
        }

        public async Task<UserDto?> UpdateDonorAsync(int id, UpdateUserDto dto)
        {
            var donor = await _repo.GetByIdAsync(id);
            if (donor == null || donor.Role != UserRole.Donor)
                return null;

            donor.UserName = dto.FullName;
            donor.Email = dto.Email;

            await _repo.UpdateAsync(donor);
            return _mapper.Map<UserDto>(donor);
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            var donor = await _repo.GetByIdAsync(id);
            if (donor == null || donor.Role != UserRole.Donor)
                return false;

            await _repo.DeleteAsync(donor);
            return true;
        }
    }
}

