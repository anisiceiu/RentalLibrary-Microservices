using Borrowing.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Borrowing.API.Data
{
    public class BorrowContext : DbContext
    {
        public BorrowContext(DbContextOptions<BorrowContext> options):base(options)
        {

        }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Return> Returns { get; set; }
    }
}
