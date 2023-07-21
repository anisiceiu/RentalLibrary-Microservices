using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories
{
    public class BindingRepository : IBindingRepository
    {
        private readonly CatalogContext _context;
        public BindingRepository(CatalogContext context)
        {
            _context = context;
        }
        public async Task CreateBindingAsync(Binding binding)
        {
            _context.Bindings.Add(binding);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBindingAsync(int id)
        {
            var catergory = await _context.Bindings.FirstOrDefaultAsync(c => c.Id == id);

            if (catergory == null)
            {
                return false;
            }
            else
            {
                _context.Bindings.Remove(catergory);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<Binding>> GetBindingsAsync()
        {
            return await _context.Bindings.ToListAsync();
        }

        public async Task<bool> UpdateBindingAsync(Binding binding)
        {
            var entity = await _context.Catergories.FirstOrDefaultAsync(m => m.Id == binding.Id);

            if (entity != null)
            {
                entity.Name = binding.Name;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
