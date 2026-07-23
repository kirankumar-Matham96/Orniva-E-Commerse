using Microsoft.EntityFrameworkCore;
using OrnivaApi.Common;
using OrnivaApi.Data;
using OrnivaApi.DTOs.Common;
using OrnivaApi.Entities;
using OrnivaApi.Repositories.Interfaces;

namespace OrnivaApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrnivaDbContext _context;

        public ProductRepository(OrnivaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> GetAll(ProductQueryParameters queryParameters)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            // Searching
            if (!string.IsNullOrWhiteSpace(queryParameters.Search))
            {
                string search = queryParameters.Search.Trim().ToLower();

                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    (p.Brand ?? string.Empty).ToLower().Contains(search) ||
                    p.SKU.Contains(search));
            }

            // Category
            if (queryParameters.CategoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryId == queryParameters.CategoryId.Value);
            }

            // Brand
            if (!string.IsNullOrWhiteSpace(queryParameters.Brand))
            {
                query = query.Where(p =>
                    p.Brand != null &&
                    p.Brand.Contains(queryParameters.Brand));
            }

            // Price Range
            if (queryParameters.MinPrice.HasValue)
            {
                query = query.Where(p =>
                    p.Price >= queryParameters.MinPrice.Value);
            }

            if (queryParameters.MaxPrice.HasValue)
            {
                query = query.Where(p =>
                    p.Price <= queryParameters.MaxPrice.Value);
            }

            // Stock
            if (queryParameters.InStock == true)
            {
                query = query.Where(p =>
                    p.StockQuantity > 0);
            }

            // Sorting
            query = (queryParameters.SortBy?.ToLower(), queryParameters.SortOrder.ToLower()) switch
            {
                ("name", "desc") => query.OrderByDescending(p => p.Name),
                ("name", _) => query.OrderBy(p => p.Name),

                ("price", "desc") => query.OrderByDescending(p => p.Price),
                ("price", _) => query.OrderBy(p => p.Price),

                ("stock", "desc") => query.OrderByDescending(p => p.StockQuantity),
                ("stock", _) => query.OrderBy(p => p.StockQuantity),

                ("brand", "desc") => query.OrderByDescending(p => p.Brand),
                ("brand", _) => query.OrderBy(p => p.Brand),

                _ => query.OrderBy(p => p.Id)
            };

            int totalRecords = await query.CountAsync();

            var products = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = products,
                TotalRecords = totalRecords,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<Product?> GetBySKU(string sku)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.SKU == sku);
        }

        public async Task<Product?> GetBySlug(string slug)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task Add(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetBySKUExceptId(string sku, int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.SKU == sku && p.Id != id);
        }

        public Task<Product?> GetBySlugExceptId(string slug, int id)
        {
            return _context.Products.FirstOrDefaultAsync(p => p.Slug == slug && p.Id != id);
        }
    }
}