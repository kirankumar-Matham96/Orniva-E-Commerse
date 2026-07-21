using OrnivaApi.DTOs.Category;
using OrnivaApi.Entities;
using OrnivaApi.Exceptions;
using OrnivaApi.Repositories.Interfaces;
using OrnivaApi.Services.Interfaces;

namespace OrnivaApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private static CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }

        public async Task<CategoryDto> Create(CreateCategoryDto dto)
        {
            var existingCategory = await _categoryRepository.GetByName(dto.Name);

            if (existingCategory != null)
                throw new Exception("Category already exists.");

            var category = new Category()
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true,
                UpdatedAt = DateTime.UtcNow
            };

            await _categoryRepository.Add(category);
            await _categoryRepository.SaveChanges();

            return MapToDto(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Select(cat => MapToDto(cat));
        }

        public async Task<CategoryDto?> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                throw new NotFoundException($"Category with the id {id} is not found");

            return MapToDto(category);
        }

        public async Task<bool> Update(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                throw new NotFoundException($"Category with the id {id} is not found");

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);

            return await _categoryRepository.SaveChanges() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                throw new NotFoundException($"Category with the id {id} is not found");

            category.IsActive = false;
            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);
            var rowsAffected = await _categoryRepository.SaveChanges();

            return rowsAffected > 0;
        }
    }
}
