using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Mvc;
using ChineseSaleApi.DTO.ChineseSaleApi.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(CreateTicketDto dto)
        {
            return Ok(_service.Create(dto));
        }
        [HttpGet("gift/{giftId}")]
        public IActionResult GetByGift(int giftId)
        {
            return Ok(_service.GetByGift(giftId));
        }

        [HttpGet("buyer/{buyerId}")]
        public IActionResult GetByBuyer(int buyerId)
        {
            return Ok(_service.GetByBuyer(buyerId));
        }

        [HttpGet("admin")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
