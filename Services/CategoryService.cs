using AutoMapper;
using OrnivaApi.DTOs.Category;
using OrnivaApi.Entities;
using OrnivaApi.Repositories.Interfaces;
using OrnivaApi.Services.Interfaces;

namespace OrnivaApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Create(CreateCategoryDto dto)
        {
            var existingCategory = await _categoryRepository.GetByName(dto.Name);

            if (existingCategory != null)
                throw new Exception("Category already exists.");

            var category = _mapper.Map<Category>(dto);

            await _categoryRepository.Add(category);

            await _categoryRepository.SaveChanges();

            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> Update(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return false;

            _mapper.Map(dto, category);

            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);

            await _categoryRepository.SaveChanges();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return false;

            category.IsActive = false;
            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);

            var rowsAffected = await _categoryRepository.SaveChanges();

            return rowsAffected > 0;
        }
    }
}
