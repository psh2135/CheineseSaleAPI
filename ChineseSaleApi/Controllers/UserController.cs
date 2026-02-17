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

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<UserDto> Register(CreateUserDto dto)
        {
            try
            {
                // ולידציה בסיסית
                if (string.IsNullOrWhiteSpace(dto.UserName))
                {
                    return BadRequest(new { message = "שם משתמש הוא שדה חובה" });
                }

                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    return BadRequest(new { message = "אימייל הוא שדה חובה" });
                }

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    return BadRequest(new { message = "סיסמה היא שדה חובה" });
                }

                var user = _service.Register(dto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                // שגיאה עסקית (כמו מייל קיים)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // שגיאה כללית
                return StatusCode(500, new { message = "אירעה שגיאה בהרשמה" });
            }
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

        [Authorize(Roles = "Admin")]
        [HttpPut("donor/{id}")]
        public async Task<ActionResult<UserDto>> UpdateDonor(int id, UpdateUserDto dto)
        {
            var updated = await _service.UpdateDonorAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = "תורם לא נמצא" });

            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("donor/{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var success = await _service.DeleteDonorAsync(id);
            if (!success)
                return NotFound(new { message = "תורם לא נמצא" });

            return NoContent();
        }

        public class LoginRequest { public string Email { get; set; } public string Password { get; set; } }
    }
}