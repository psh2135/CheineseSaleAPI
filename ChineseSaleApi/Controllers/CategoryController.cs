using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTOs;
using ChineseSaleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChineseSaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service) => _service = service;

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            return Ok(_service.GetAllCategories());
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = _service.GetCategoryById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CategoryDto> Post(CreateCategoryDto dto)
        {
            var created = _service.CreateCategory(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}
