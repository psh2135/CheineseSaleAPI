using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public IActionResult RunLottery([FromBody] RunLotteryDto dto)
        {
            var result = _service.RunLottery(dto);
            return Ok(result);
        }
        [HttpGet("results")]
        public IActionResult GetAllWinners()
        {
            var results = _service.GetAllLotteryResults();
            return Ok(results);
        }
    }
}
