using OrnivaApi.Entities;

namespace OrnivaApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product?> GetById(int id);

        Task<Product?> GetBySKU(string sku);

        Task<Product?> GetBySlug(string slug);

        Task<Product?> GetBySKUExceptId(string sku, int id);

        Task<Product?> GetBySlugExceptId(string slug, int id);

        Task Add(Product product);

        void Update(Product product);

        Task<int> SaveChanges();
    }
}
