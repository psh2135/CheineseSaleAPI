using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }
        [HttpPost(Name = "CreateUser")]
        [Authorize(Roles = "Manager")]
        public IActionResult CreateUser(CreateUserDto user)
        {
            CreateUserDto result = _userService.CreateUser(user);
            return Ok(result);
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDto user)
        {
            if (id != user.Id)
                return BadRequest();
            var updatedUser = _userService.UpdateUser(user);
            if (updatedUser == null)
                return NotFound(); 
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var userToDelete = _userService.DeleteUser(new UserDto { Id = id });
            if (userToDelete == null)
                return NotFound();

            return Ok(userToDelete);
        }
    }
}
