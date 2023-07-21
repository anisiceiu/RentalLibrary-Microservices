using Member.API.Entities;

namespace Member.API.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<MemberDetail>> GetMembersAsync();
        Task<MemberDetail?> GetMemberByIdAsync(int id);
        Task<MemberDetail> AddMemberAsync(MemberDetail member);
    }
}
