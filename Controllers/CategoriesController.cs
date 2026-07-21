using Microsoft.AspNetCore.Mvc;
using OrnivaApi.DTOs.Category;
using OrnivaApi.Services.Interfaces;

namespace OrnivaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        {
            var category = await _categoryService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var updated = await _categoryService.Update(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
