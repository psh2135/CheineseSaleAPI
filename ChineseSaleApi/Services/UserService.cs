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
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(IUserRepository repo, IMapper mapper, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
        }

        public UserDto Register(CreateUserDto dto) => CreateUserWithRole(dto, UserRole.Buyer);

        public UserDto AddDonor(CreateUserDto dto) => CreateUserWithRole(dto, UserRole.Donor);

        private UserDto CreateUserWithRole(CreateUserDto dto, UserRole role)
        {
            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = role;

            _repo.Add(user);
            _repo.Save();
            return _mapper.Map<UserDto>(user);
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
    }
}
