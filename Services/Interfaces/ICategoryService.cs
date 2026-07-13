using OrnivaApi.DTOs.Category;

namespace OrnivaApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAll();

        Task<CategoryDto?> GetById(int id);

        Task<CategoryDto> Create(CreateCategoryDto dto);

        Task<bool> Update(int id, UpdateCategoryDto dto);

        Task<bool> Delete(int id);
    }
}
