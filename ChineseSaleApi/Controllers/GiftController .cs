//using ChineseSaleApi.DTO;
//using ChineseSaleApi.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace ChineseSaleApi.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class GiftController : ControllerBase
//    {
//        private readonly IGiftService _GiftService;
//        public GiftController(IGiftService GiftService) {
//            _GiftService = GiftService;
//        }
//        [HttpPost(Name = "CreateGift")]
//        public IActionResult CreateGift(CreateGiftDto Gift)
//        {
//            CreateGiftDto result = _GiftService.CreateGift(Gift);
//            return Ok(result);
//        }

//        [HttpGet("all")]
//        public IActionResult GetAllGifts()
//        {
//            var Gifts = _GiftService.GetAllGifts();
//            return Ok(Gifts);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetGiftById(int id)
//        {
//            var Gift = _GiftService.GetGiftById(id);
//            if (Gift == null)
//                return NotFound();
//            return Ok(Gift);
//        }
//        [HttpPut("{id}")]
//        public IActionResult UpdateGift(int id, GiftDto Gift)
//        {
//            if (id != Gift.Id)
//                return BadRequest();
//            var updatedGift = _GiftService.UpdateGift(Gift);
//            if (updatedGift == null)
//                return NotFound(); 
//            return Ok(updatedGift);
//        }
//        [HttpDelete("{id}")]
//        public IActionResult DeleteGift(int id)
//        {
//            var GiftToDelete = _GiftService.DeleteGift(new GiftDto { Id = id });
//            if (GiftToDelete == null)
//                return NotFound();

//            return Ok(GiftToDelete);
//        }
//    }
//}
using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChineseSaleApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftsController(IGiftService giftService)
        {
            _giftService = giftService;
        }
        //כולם
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GiftDto>> GetAllGifts()
        {
            var gifts = _giftService.GetAllGifts();
            return Ok(gifts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<GiftDto> GetGiftById(int id)
        {
            var gift = _giftService.GetGiftById(id);
            if (gift == null)
                return NotFound();

            return Ok(gift);
        }

        [HttpGet("category/{categoryId}")]
        [AllowAnonymous]
        public IActionResult GetByCategory(int categoryId)
        {
            var results = _giftService.GetGiftsByCategory(categoryId);
            return Ok(results);
        }
        [HttpGet("winner/{id}")]
        [AllowAnonymous]
        public ActionResult<UserDto> GetWinner(int id)
        {
            var winner = _giftService.GetWinner(id);

            if (winner == null)
                return NotFound($"No winner found for gift with id {id}");

            return Ok(winner);
        }
        [HttpGet("winners")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<WinnerGiftDto>>> GetAllWinners()
        {
            var winners = await _giftService.GetAllWinnersAsync();
            return Ok(winners);
        }

        //רק מנהל
        [HttpPost]
        public ActionResult<GiftDto> CreateGift([FromBody] CreateGiftDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _giftService.CreateGift(dto);
            return CreatedAtAction(nameof(GetGiftById), new { id = result.Id }, result);
        }
       

        [HttpPut("{id}")]
        public ActionResult<GiftDto> UpdateGift(int id, [FromBody] UpdateGiftDto dto)
        {
          var updated = _giftService.UpdateGift(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGift(int id)
        {
            var existing = _giftService.GetGiftById(id);
            if (existing == null)
                return NotFound();

            _giftService.DeleteGift(id);
            return NoContent();
        }
        [HttpGet("most-popular")]
        public IActionResult GetMostPopularGifts()
        {
            var stats = _giftService.GetMostPopularGifts();
            return Ok(stats);
        }
    }
}
