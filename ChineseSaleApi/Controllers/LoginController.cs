//using ChineseSaleApi.DTO;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace ChineseSaleApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class LoginController : ControllerBase
//    {
//        [HttpPost(Name = "LoginManager")]
//        public IActionResult LoginManager(string userName, string password)
//        {
//            //check db user
//            bool isManager = true;
//            string token="";
//            if (isManager)
//                token=GenerateJwt();
//            return Ok(token);
//        }

//        private string GenerateJwt()
//        {
//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Name, "managerUser"),
//                new Claim(ClaimTypes.Role, "Manager")
//            };

//            var key = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_123456_SUPER_SECRET_KEY_123456"));

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: "MyApp",
//                audience: "MyApp",
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(1),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
