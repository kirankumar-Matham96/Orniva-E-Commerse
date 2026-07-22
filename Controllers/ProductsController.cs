using Microsoft.AspNetCore.Mvc;
using OrnivaApi.DTOs.Product;
using OrnivaApi.Responses;
using OrnivaApi.Services.Interfaces;

namespace OrnivaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAll();

            return Ok(
                    new ApiResponse<IEnumerable<ProductDto>>(
                            true,
                            "Products retreived successfully",
                            products
                        )
                );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetById(id);

            return Ok(
                    new ApiResponse<ProductDto>(
                            true,
                            $"Product with id {id} is retreived successfully",
                            product
                        )
                );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                new ApiResponse<ProductDto>(
                        true,
                        "Product added successfully",
                        product
                    )
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var result = await _service.Update(id, dto);

            return Ok(
                    new ApiResponse<Object>(
                            true,
                            $"Product with id {id} is updated successfuly",
                            null
                        )
                );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);

            return Ok(
                    new ApiResponse<Object>(
                            true,
                            $"Product with id {id} is deleted successfuly",
                            null
                        )
                );
        }
    }
}
