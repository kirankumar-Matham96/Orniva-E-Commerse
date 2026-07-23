using OrnivaApi.DTOs.Common;
using OrnivaApi.DTOs.Product;
using OrnivaApi.Responses;

namespace OrnivaApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<IEnumerable<ProductDto>>> GetAll(ProductQueryParameters queryParameters);

        Task<ProductDto?> GetById(int id);

        Task<ProductDto> Create(CreateProductDto dto);

        Task<bool> Update(int id, UpdateProductDto dto);

        Task<bool> Delete(int id);
    }
}