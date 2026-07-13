using Microsoft.EntityFrameworkCore;
using OrnivaApi.Data;
using OrnivaApi.Entities;
using OrnivaApi.Repositories.Interfaces;

namespace OrnivaApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OrnivaDbContext _dbContext;

        public CategoryRepository(OrnivaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.AsNoTracking().Where(cat => cat.IsActive).OrderBy(cat => cat.Name).ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(cat => cat.Id == id && cat.IsActive);
        }

        public async Task<Category?> GetByName(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(cat => cat.Name == name && cat.IsActive && cat.Name.ToLower() == name.ToLower());
        }

        public async Task Add(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }
        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }

        //public void Delete(Category category)
        //{
        //    _dbContext.Categories.Remove(category);
        //}

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Categories.AnyAsync(cat => cat.Id == id);
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
