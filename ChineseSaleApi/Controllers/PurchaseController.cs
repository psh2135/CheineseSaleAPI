using ChineseSaleApi.DTO;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleApi.Controllers
{
    [ApiController]
    [Route("api/purchases")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePurchaseDto dto)
        {
            var result = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
