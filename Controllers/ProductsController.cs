using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrnivaApi.DTOs.Product;
using OrnivaApi.Services;
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
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetById(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var result = await _service.Update(id, dto);

            return !result ? NotFound() : Content("Updated successfully");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);

            return !result ? NotFound() : Content("Deleted successfully");
        }
    }
}
