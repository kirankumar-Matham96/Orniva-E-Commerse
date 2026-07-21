using Microsoft.EntityFrameworkCore;
using OrnivaApi.Data;
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

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .ToListAsync();
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
            return await _context.Products.FirstOrDefaultAsync(p => p.SKU == sku && p.Id == id);
        }

        public Task<Product?> GetBySlugExceptId(string slug, int id)
        {
            return _context.Products.FirstOrDefaultAsync(p => p.Slug == slug && p.Id == id);
        }
    }
}