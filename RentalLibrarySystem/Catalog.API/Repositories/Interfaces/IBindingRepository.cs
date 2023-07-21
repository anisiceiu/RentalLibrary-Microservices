using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IBindingRepository
    {
        Task<IEnumerable<Binding>> GetBindingsAsync();
        Task CreateBindingAsync(Binding binding);
        Task<bool> UpdateBindingAsync(Binding binding);
        Task<bool> DeleteBindingAsync(int id);
    }
}
