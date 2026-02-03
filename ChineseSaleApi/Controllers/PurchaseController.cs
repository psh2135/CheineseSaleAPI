using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly IPurchaseService _service;
        public CartController(IPurchaseService service) => _service = service;

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddToCartDto dto)
        {
            _service.AddToCart(dto);
            return Ok(new { message = "נוסף לסל בהצלחה" });
        }

        [HttpPost("checkout/{buyerId}")]
        public IActionResult Checkout(int buyerId)
        {
            var purchase = _service.CompletePurchase(buyerId);
            return Ok(purchase);
        }

        [HttpGet("{id}")]
        public IActionResult GetPurchase(int id)
        {
            var res = _service.GetById(id);
            return res != null ? Ok(res) : NotFound();
        }

        [HttpDelete("remove")]
        public IActionResult RemoveFromCart([FromBody] AddToCartDto dto)
        {
            _service.RemoveFromCart(dto);
            return Ok(new { message = "הכרטיס הוסר מהסל בהצלחה" });
        }
    }
}