using OrnivaApi.DTOs.Product;
using OrnivaApi.Entities;
using OrnivaApi.Repositories.Interfaces;
using OrnivaApi.Services.Interfaces;

namespace OrnivaApi.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;

        public ProductService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ProductDto MapToDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                SKU = product.SKU,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                StockQuantity = product.StockQuantity,
                Brand = product.Brand,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty
            };
        }

        public async Task<ProductDto> Create(CreateProductDto dto)
        {
            // check the existingSku
            var existingSku = await _productRepository.GetBySKU(dto.SKU);

            if (existingSku != null)
                throw new Exception("SKU already exists");

            // check Slug
            var existingSlug = await _productRepository.GetBySlug(dto.Slug);

            if (existingSlug != null)
                throw new Exception("SLUG already exists");

            // check category
            var category = await _categoryRepository.GetById(dto.CategoryId);

            if (category == null)
                throw new Exception("Category is not found");

            // create new product
            var product = new Product()
            {
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                ShortDescription = dto.ShortDescription,
                Price = dto.Price,
                DiscountPrice = dto.DiscountPrice,
                StockQuantity = dto.StockQuantity,
                Brand = dto.Brand,
                SKU = dto.SKU,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,

                IsActive = true,
                UpdatedAt = DateTime.UtcNow
            };

            await _productRepository.Add(product);
            await _productRepository.SaveChanges();

            // load the category name for DTO
            product.Category = category;
            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();

            return products.Select(MapToDto);
        }

        public async Task<ProductDto?> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            return product != null ? MapToDto(product) : null;
        }

        public async Task<bool> Update(int id, UpdateProductDto dto)
        {
            // Get product
            var product = await _productRepository.GetById(id);

            if (product == null)
                return false;

            // Check category exists
            var category = await _categoryRepository.GetById(dto.CategoryId);

            if (category == null)
                throw new Exception("Category not found");

            // Check duplicate Sku
            var existingSku = await _productRepository.GetBySKUExceptId(product.SKU, id);

            if (existingSku != null)
                throw new Exception("SKU already exists");

            // Check duplicate Slug
            var existsingSlug = await _productRepository.GetBySlugExceptId(product.Slug, id);

            if (existsingSlug != null)
                throw new Exception("Slug already exists");

            // Update
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.ShortDescription = dto.ShortDescription;
            product.Brand = dto.Brand;
            product.Price = dto.Price;
            product.DiscountPrice = dto.DiscountPrice;
            product.Slug = dto.Slug;
            product.SKU = dto.SKU;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;

            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);

            return await _productRepository.SaveChanges() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return false;

            product.IsActive = false;

            return await _productRepository.SaveChanges() > 0;
        }
    }
}
