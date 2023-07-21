using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class CatalogContext:DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options):base(options)
        {

        }

        public DbSet<Binding> Bindings { get;set; }
        public DbSet<Category> Catergories { get;set; }
        public DbSet<Book> Books { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasIndex(c => c.ISBN).IsUnique();
        }
    }
}
