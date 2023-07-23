using Borrowing.API.Data;
using Borrowing.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Borrowing.API.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly BorrowContext _context;
        public BorrowRepository(BorrowContext context)
        {
            _context = context;
        }

        public async Task<Request> BookRenewRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public async Task<Request> BookReserveRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Request>> GetAllBookRequest()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<List<Borrow>> GetAllBorrowedBookByMemberId(int memberId)
        {
            return await _context.Borrows.Where(c=> c.MemberId == memberId).ToListAsync();
        }

        public async Task<List<Borrow>> GetAllOverDueBorrowedBook()
        {
            throw new NotImplementedException();
        }

        public async Task<Borrow?> GetBorrowById(int borrowId)
        {
            return await _context.Borrows.Where(c => c.Id == borrowId).FirstOrDefaultAsync();
        }

        public async Task<Request?> GetRequestById(int requestId)
        {
            return await _context.Requests.Where(c => c.Id == requestId).FirstOrDefaultAsync();
        }

        public async Task<Return?> GetReturnById(int returnId)
        {
            return await _context.Returns.Where(c=> c.Id == returnId).FirstOrDefaultAsync();
        }

        public async Task<Borrow> IssueBook(int userId, Request request)
        {
            throw new NotImplementedException();
        }

        public async Task<Return> ReturnBook(int userId, Borrow borrow)
        {
            throw new NotImplementedException();
        }
    }
}
