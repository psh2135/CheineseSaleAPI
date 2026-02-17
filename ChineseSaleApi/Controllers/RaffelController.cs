//using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("api/[controller]")]
//public class RaffleController : ControllerBase
//{
//    private readonly IRaffleService _raffleService;

//    public RaffleController(IRaffleService raffleService)
//    {
//        _raffleService = raffleService;
//    }

//    [HttpGet]
//    public async Task<IActionResult> Get()
//    {
//        var raffle = await _raffleService.GetAsync();

//        if (raffle == null)
//            return NotFound();

//        return Ok(raffle);
//    }

//    [HttpPost("start")]
//    public async Task<IActionResult> Start()
//    {
//        await _raffleService.StartAsync();
//        return Ok();
//    }
//}
