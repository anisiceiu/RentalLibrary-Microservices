using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _context;
        public CategoryRepository(CatalogContext context)
        {
            _context = context;
        }
        public async Task CreateCatergoryAsync(Category  catergory)
        {
            _context.Catergories.Add(catergory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCatergoryAsync(int id)
        {
            var catergory = await _context.Catergories.FirstOrDefaultAsync(c => c.Id == id);

            if (catergory == null)
            {
                return false;
            }
            else
            {
                _context.Catergories.Remove(catergory);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<Category>> GetCatergoriesAsync()
        {
            return await _context.Catergories.ToListAsync();
        }

        public async Task<bool> UpdateCatergoryAsync(Category catergory)
        {
            var entity = await _context.Catergories.FirstOrDefaultAsync(m => m.Id == catergory.Id);

            if (entity != null)
            {
                entity.Name = catergory.Name;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
