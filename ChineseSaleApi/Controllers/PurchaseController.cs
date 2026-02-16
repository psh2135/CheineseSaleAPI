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
            if (!TryGetUserId(out int buyerId))
                return Unauthorized();

            await _service.AddToCartAsync(buyerId, dto);

            return Ok(new { message = "נוסף לסל בהצלחה" });
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            if (!TryGetUserId(out int buyerId))
                return Unauthorized();

            var purchase = await _service.CompletePurchase(buyerId);
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
            if (!TryGetUserId(out int buyerId))
                return Unauthorized();

           _service.RemoveFromCart(buyerId, dto);

            return Ok(new { message = "הכרטיס הוסר מהסל בהצלחה" });
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            if (!TryGetUserId(out int buyerId))
                return Unauthorized();

            var cart = await _service.GetCartAsync(buyerId);
            return Ok(cart);
        }

        [HttpGet("my-tickets")]
        [Authorize]
        public IActionResult GetMyTickets()
        {
            if (!TryGetUserId(out int buyerId))
                return Unauthorized();

            var tickets =  _service.GetUserTickets(buyerId);
            return Ok(tickets);
        }

        [HttpGet("dashboard-stats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var data = await _service.GetAdminDashboardStats();
            return Ok(data);
        }
        private bool TryGetUserId(out int userId)
        {
            userId = 0;

            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null && int.TryParse(claim.Value, out userId);
        }
    }
}