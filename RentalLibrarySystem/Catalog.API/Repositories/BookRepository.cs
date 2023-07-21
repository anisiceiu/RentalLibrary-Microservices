using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly CatalogContext _context;
        public BookRepository(CatalogContext context)
        {
            _context = context;
        }
        public async Task CreateBookAsync(Book Book)
        {
            _context.Books.Add(Book);
           await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
          var book = await _context.Books.FirstOrDefaultAsync(c=> c.Id == id);
            
            if (book == null)
            {
                return false;
            }
            else
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
               
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            var book = await _context.Books
                .Include(c=> c.Binding)
                .Include(c=> c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            return book;
        }

        public async Task<Book?> GetBookByISBNAsync(string isbn)
        {
            var book = await _context.Books
                     .Include(c => c.Binding)
                     .Include(c => c.Category)
                     .FirstOrDefaultAsync(m => m.ISBN == isbn);
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
           return await _context.Books.Include(c=> c.Category).Include(b=> b.Binding).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByBindingAsync(int bindingId)
        {
            return await _context.Books.Include(c => c.Category).Include(b => b.Binding)
                .Where(c=> c.BindingId == bindingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books.Include(c => c.Category).Include(b => b.Binding)
                .Where(c=> c.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var entity = await _context.Books.FirstOrDefaultAsync(m=> m.Id == book.Id);

            if(entity != null)
            {
                entity.Title = book.Title;
                entity.ISBN = book.ISBN;
                entity.NoOfCopies = book.NoOfCopies;
                entity.NoOfAvailableCopies = book.NoOfAvailableCopies;
                entity.BindingId = book.BindingId;
                entity.CategoryId = book.CategoryId;
                entity.Language = book.Language;
                entity.PublishYear = book.PublishYear;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
