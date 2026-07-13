using OrnivaApi.Entities;

namespace OrnivaApi.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task<Category?> GetById(int id);

        Task<Category?> GetByName(string name);

        Task Add(Category category);

        void Update(Category category);

        //void Delete(Category category);

        Task<bool> Exists(int id);

        Task<int> SaveChanges();
    }
}
