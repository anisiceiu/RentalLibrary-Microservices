using Member.API.Data;
using Member.API.Entities;
using Member.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Member.API.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MemberContext _memberContext;
        public MemberRepository(MemberContext memberContext)
        {
            _memberContext = memberContext;
        }
        public async Task<MemberDetail> AddMemberAsync(MemberDetail member)
        {
            await _memberContext.MemberDetails.AddAsync(member);
            await _memberContext.SaveChangesAsync();
            return member;
        }

        public async Task<MemberDetail?> GetMemberByIdAsync(int id)
        {
            var member = await _memberContext.MemberDetails.FirstOrDefaultAsync(c => c.Id == id);
            return member;
        }

        public async Task<List<MemberDetail>> GetMembersAsync()
        {
            var members = await _memberContext.MemberDetails.ToListAsync();
            return members;
        }
    }
}
