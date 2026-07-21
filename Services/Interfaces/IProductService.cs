using OrnivaApi.DTOs.Product;

namespace OrnivaApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();

        Task<ProductDto?> GetById(int id);

        Task<ProductDto> Create(CreateProductDto dto);

        Task<bool> Update(int id, UpdateProductDto dto);

        Task<bool> Delete(int id);
    }
}