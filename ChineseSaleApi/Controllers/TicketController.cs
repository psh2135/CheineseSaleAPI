using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Mvc;
using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        
        [HttpGet("gift/{giftId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByGift(int giftId)
        {
            return Ok(_service.GetByGift(giftId));
        }


        [HttpGet("admin")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
