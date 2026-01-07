using ChineseSaleApi.DTO;
using ChineseSaleApi.Repositories;
using System.Collections.Generic;

namespace ChineseSaleApi.Services
{
    public interface IUserService
    {
        CreateUserDto CreateUser(CreateUserDto user);
        UserDto GetUserById(int id);        
        UserDto UpdateUser(UserDto user);
        UserDto DeleteUser(int id);           
        IEnumerable<UserDto> GetAllUsers();
        object DeleteUser(UserDto userDto);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CreateUserDto CreateUser(CreateUserDto user)
        {
            return _userRepository.CreateUser(user);
        }

        public UserDto GetUserById(int id)   
        {
            return _userRepository.GetUserById(id);
        }

        public UserDto UpdateUser(UserDto user)
        {
            return _userRepository.UpdateUser(user);
        }

        public UserDto DeleteUser(int id)  
        {
            return _userRepository.DeleteUser(id);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public object DeleteUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
