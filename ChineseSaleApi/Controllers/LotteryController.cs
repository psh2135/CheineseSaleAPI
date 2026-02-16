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
        private readonly IRaffleStateService _stateService;


        public LotteryController(ILotteryService service, IRaffleStateService stateService)
        {
            _service = service;
            _stateService = stateService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("start")]
        public IActionResult StartRaffle()
        {
            _stateService.StartRaffle();
            return Ok();
        }
        [HttpGet("status")]
        public IActionResult GetRaffleStatus()
        {
            return Ok(new
            {
                isLocked = _stateService.IsRaffleLocked()
            });
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
