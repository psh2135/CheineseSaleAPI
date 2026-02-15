using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IPurchaseService _service;
        public CartController(IPurchaseService service) => _service = service;

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (buyerId == null)
                return Unauthorized();

            await _service.AddToCartAsync(int.Parse(buyerId), dto);

            return Ok(new { message = "נוסף לסל בהצלחה" });
        }

        [HttpPost("checkout/{buyerId}")]
        public IActionResult Checkout(int buyerId)
        {
            var purchase = _service.CompletePurchase(buyerId);
            return Ok(purchase);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPurchase(int id)
        {
            var res = _service.GetById(id);
            return res != null ? Ok(res) : NotFound();
        }

        [HttpDelete("remove")]
        public IActionResult RemoveFromCart([FromBody] AddToCartDto dto)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (buyerId == null)
                return Unauthorized();

            _service.RemoveFromCart(int.Parse(buyerId), dto );
            return Ok(new { message = "הכרטיס הוסר מהסל בהצלחה" });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GiftDto>> GetAllPackages()
        {
            var gifts = _service.GetAllPackages();
            return Ok(gifts);
        }

        [HttpGet("my-tickets")]
        [Authorize]
        public IActionResult GetMyTickets()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return Unauthorized();

            int buyerId = int.Parse(userIdClaim.Value);

            var tickets = _service.GetUserTickets(buyerId);
            return Ok(tickets);
        }
        [HttpGet("dashboard-stats")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDashboardStats()
        {
            return Ok(_service.GetAdminDashboardStats());
        }
    }
}