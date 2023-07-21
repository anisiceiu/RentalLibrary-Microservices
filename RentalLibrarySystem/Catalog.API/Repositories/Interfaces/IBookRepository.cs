using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book?> GetBookAsync(int id);
        Task<Book?> GetBookByISBNAsync(string isbn);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<IEnumerable<Book>> GetBooksByBindingAsync(int bindingId);

        Task CreateBookAsync(Book Book);
        Task<bool> UpdateBookAsync(Book Book);
        Task<bool> DeleteBookAsync(int id);
    }
}
