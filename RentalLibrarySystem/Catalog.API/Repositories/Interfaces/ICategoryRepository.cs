using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCatergoriesAsync();
        Task<Category> CreateCatergoryAsync(Category  catergory);
        Task<bool> UpdateCatergoryAsync(Category catergory);
        Task<bool> DeleteCatergoryAsync(int id);
    }
}
