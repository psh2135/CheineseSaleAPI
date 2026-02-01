//using ChineseSaleApi.DTO;
//using ChineseSaleApi.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ChineseSaleApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IUserService _service;
//        public AuthController(IUserService service) => _service = service;

//        [HttpPost("register")]
//        public ActionResult<UserDto> Register(CreateUserDto dto)
//        {
//            var user = _service.Register(dto);
//            return Ok(user);
//        }

//        [HttpPost("add-donor")]
//        // בדרך כלל כאן תרצה להוסיף [Authorize(Roles = "Admin")]
//        public ActionResult<UserDto> AddDonor(CreateUserDto dto)
//        {
//            var user = _service.AddDonor(dto);
//            return Ok(user);
//        }

//        [HttpGet("donors")]
//        public ActionResult<IEnumerable<UserDto>> GetDonors()
//        {
//            var donors = _service.GetAllDonors();
//            return Ok(donors);
//        }

//        [HttpPost("login")]
//        public ActionResult<UserDto> Login([FromBody] LoginRequest login)
//        {
//            var user = _service.Login(login.Email, login.Password);
//            if (user == null) return Unauthorized("אימייל או סיסמה שגויים");
//            return Ok(user);
//        }
//    }

//    // מחלקה פשוטה להתחברות
//    public class LoginRequest { public string Email { get; set; } public string Password { get; set; } }
//}
using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        public AuthController(IUserService service) => _service = service;

        // הרשמה פתוחה לכולם - אין צורך בטוקן
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<UserDto> Register(CreateUserDto dto)
        {
            var user = _service.Register(dto);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserDto> Login([FromBody] LoginRequest login)
        {
            var user = _service.Login(login.Email, login.Password);
            if (user == null) return Unauthorized(new { message = "אימייל או סיסמה שגויים" });
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-donor")]
        public ActionResult<UserDto> AddDonor(CreateUserDto dto)
        {
            var user = _service.AddDonor(dto);
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("donors")]
        public ActionResult<IEnumerable<UserDto>> GetDonors()
        {
            var donors = _service.GetAllDonors();
            return Ok(donors);
        }
    }

    public class LoginRequest { public string Email { get; set; } public string Password { get; set; } }
}