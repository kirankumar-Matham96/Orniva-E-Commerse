using Microsoft.AspNetCore.Mvc;
using OrnivaApi.DTOs.Category;
using OrnivaApi.Responses;
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

            return Ok(
                    new ApiResponse<IEnumerable<CategoryDto>>(
                        true,
                        "Categories retreived successfully",
                        categories
                    )
                );
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryService.GetById(id);

            return Ok(
                    new ApiResponse<CategoryDto>(
                        true,
                        $"Category with id {id} is retreived successfully",
                        category
                    )
                );
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
        {
            var category = await _categoryService.Create(dto);

            return CreatedAtAction(
                            nameof(GetById),
                            new { id = category.Id },
                            new ApiResponse<CategoryDto>(
                                    true,
                                    "Category created successfully",
                                    category
                                ));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var updated = await _categoryService.Update(id, dto);

            return Ok(
                    new ApiResponse<Object>(
                            true,
                            $"Category with id {id} is updated successfully",
                            null
                        )
                );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.Delete(id);

            return Ok(
                    new ApiResponse<Object>(
                            true,
                            $"Category with id {id} is deleted successfully",
                            null
                        )
                );
        }
    }
}
