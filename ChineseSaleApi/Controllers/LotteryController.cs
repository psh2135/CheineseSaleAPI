using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryService _service;

        public LotteryController(ILotteryService service)
        {
            _service = service;
        }

        [HttpPost("run")]
        public IActionResult RunLottery([FromBody] RunLotteryDto dto)
        {
            var result = _service.RunLottery(dto);
            return Ok(result);
        }
    }
}
