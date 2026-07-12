using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductsRepository prodInstance;

        public ProductsController(ProductsRepository prodRepo)
        {
            this.prodInstance = prodRepo;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportProducts()
        {
            await this.prodInstance.ImportProductsFromApi();
            return Ok("Products imported successfully");
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductsRepository>> GetProducts()
        {
            var products = this.prodInstance.GetProducts();
            return Ok(products);
        }

        [HttpPost]
        public void AddProduct(Products product)
        {
            try
            {
                this.prodInstance.InsertProduct(product);
            }
            catch (Exception error)
            {
                Console.WriteLine("error while adding product: ", error.Message);
            }
        }

        [HttpPut]
        public void UpdateProduct(Products product)
        {
            this.prodInstance.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            this.prodInstance.DeleteRecord(id);
            return Ok();
        }
    }
}
