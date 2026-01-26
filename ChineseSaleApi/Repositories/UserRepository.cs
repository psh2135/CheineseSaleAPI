using AutoMapper;
using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Linq;

namespace ChineseSaleApi.Repositories
{
    public interface IUserRepository
    {
        CreateUserDto CreateUser(CreateUserDto user);
        UserDto GetUserById(int id);         
        UserDto UpdateUser(UserDto user);
        UserDto DeleteUser(int id);         
        IEnumerable<UserDto> GetAllUsers();
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CreateUserDto CreateUser(CreateUserDto user)
        {
            var userModel = _mapper.Map<User>(user);
            _dbContext.Users.Add(userModel);
            _dbContext.SaveChanges();

            return _mapper.Map<CreateUserDto>(userModel);
        }

        public UserDto GetUserById(int id)
        {
            var userModel = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            return userModel == null ? null : _mapper.Map<UserDto>(userModel);

        }

        public UserDto UpdateUser(UserDto user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null) return null; 

            _mapper.Map(user, existingUser);
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(existingUser);
        }

        public UserDto DeleteUser(int id)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null) return null; 

            _dbContext.Users.Remove(existingUser);
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(existingUser);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users.ToList(); 
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
