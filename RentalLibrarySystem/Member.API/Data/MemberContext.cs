using Member.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Member.API.Data
{
    public class MemberContext:DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options):base(options)
        {

        }

        public DbSet<MemberDetail> MemberDetails { get; set; }
    }
}
