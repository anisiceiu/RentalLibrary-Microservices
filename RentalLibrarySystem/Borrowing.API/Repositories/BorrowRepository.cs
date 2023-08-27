using Borrowing.API.Data;
using Borrowing.API.Entities;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace Borrowing.API.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly BorrowContext _context;
        private readonly IConfiguration _configuration;
        public BorrowRepository(BorrowContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Request> BookRenewRequestAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Request> BookReserveRequestAsync(Request request)
        {
            var client = new RestClient(_configuration.GetValue<string>("CatalogApiBaseUrl"));
            var req = new RestRequest($"/api/Catalog/GetIsBookAvailable/{request.BookId}");
            var response = client.ExecuteGet<bool>(req);

            if (response.Data)
            {
                await _context.Requests.AddAsync(request);
                await _context.SaveChangesAsync();
                return request;
            }
            else
            {
                return null;
            }

        }

        public async Task<List<Request>> GetAllBookRequestAsync()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<List<Borrow>> GetAllBorrowedBookByMemberIdAsync(int memberId)
        {
            return await _context.Borrows
                .Where(c => c.MemberId == memberId)
                .Join(_context.Requests,
                 borrow => borrow.RequestId,
                 request => request.Id,
                 (bor, req) => new Borrow
                 {
                     BookId = bor.BookId,
                     BookName = req.BookName,
                     DateBorrowed = bor.DateBorrowed,
                     DueDate = bor.DueDate,
                     Id = bor.Id,
                     MemberId = bor.MemberId,
                     RequestId = bor.RequestId,
                     Fees = bor.Fees,
                     UserId = bor.UserId
                 })
                .ToListAsync();
        }

        public async Task<List<Borrow>> GetAllOverDueBorrowedBookAsync()
        {
            return await _context.Borrows.Where(m => m.DueDate < DateTime.UtcNow).ToListAsync();
        }

        public async Task<Borrow?> GetBorrowByIdAsync(int borrowId)
        {
            return await _context.Borrows.Where(c => c.Id == borrowId).FirstOrDefaultAsync();
        }

        public async Task<Request?> GetRequestByIdAsync(int requestId)
        {
            return await _context.Requests.Where(c => c.Id == requestId).FirstOrDefaultAsync();
        }

        public async Task<Return?> GetReturnByIdAsync(int returnId)
        {
            return await _context.Returns.Where(c => c.Id == returnId).FirstOrDefaultAsync();
        }

        public async Task<Borrow?> IssueBookAsync(int userId, Request request)
        {
            if (_context.Borrows.Where(c => c.RequestId == request.Id).Any())
                return null;

            int rate = _configuration.GetValue<int>("PerDayFees");
            int days = Convert.ToInt32((request.ToDate - request.FromDate).TotalDays);
            var fees = await Task.FromResult(rate * days);

            var borrow = new Borrow
            {
                BookId = request.BookId,
                DateBorrowed = request.FromDate,
                DueDate = request.ToDate,
                Fees = fees,
                MemberId = request.MemberId,
                UserId = userId,
                RequestId = request.Id
            };

            await _context.Borrows.AddAsync(borrow);
            await _context.SaveChangesAsync();

            var req = _context.Requests.Where(c => c.Id == request.Id).FirstOrDefault();

            if (req != null)
            {
                req.RequestStatus = "Accepted";
                await _context.SaveChangesAsync();
            }

            return borrow;
        }

        public async Task<Return> ReturnBookAsync(int userId, Borrow borrow)
        {
            int fine = await CalculateFineAsync(borrow.DueDate);

            var returnbook = new Return
            {
                DueDate = borrow.DueDate,
                BorrowId = borrow.Id,
                DateReturned = DateTime.UtcNow,
                Fine = fine,
                UserId = userId
            };

            await _context.Returns.AddAsync(returnbook);
            await _context.SaveChangesAsync();

            var bor = await _context.Borrows.FirstOrDefaultAsync(c=>c.Id == borrow.Id);

            if (bor != null)
            {
                bor.IsReturned = true;
                await _context.SaveChangesAsync();
            }

            return returnbook;
        }

        public async Task<int> CalculateFineAsync(DateTime dueDate)
        {
            int fineRate = _configuration.GetValue<int>("PerDayFine");
            int delayDays = Convert.ToInt32((DateTime.UtcNow - dueDate).TotalDays);
            return await Task.FromResult(fineRate * delayDays);
        }

        public async Task<Request?> RejectRequestAsync(int requestId)
        {
            var req = await _context.Requests.Where(c => c.Id == requestId).FirstOrDefaultAsync();
            if (req == null)
                return null;
            else
            {
                req.RequestStatus = "Rejected";
                await _context.SaveChangesAsync();

                return req;
            }
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<List<Borrow>> GetAllBorrowedBookByMemberNoAsync(string memberNo)
        {
            return await _context.Borrows
                .Where(c => c.MemberNo == memberNo)
                .Join(_context.Requests,
                 borrow => borrow.RequestId,
                 request => request.Id,
                 (bor, req) => new Borrow
                 {
                     BookId = bor.BookId,
                     BookName = req.BookName,
                     DateBorrowed = bor.DateBorrowed,
                     DueDate = bor.DueDate,
                     Id = bor.Id,
                     MemberId = bor.MemberId,
                     MemberNo = bor.MemberNo,
                     RequestId = bor.RequestId,
                     Fees = bor.Fees,
                     UserId = bor.UserId
                 })
                .ToListAsync();
        }

        public async Task<List<Borrow>> GetAllBorrowedBooksAsync()
        {
            return await _context.Borrows
                .Join(_context.Requests,
                 borrow => borrow.RequestId,
                 request => request.Id,
                 (bor, req) => new Borrow
                 {
                     BookId = bor.BookId,
                     BookName = req.BookName,
                     DateBorrowed = bor.DateBorrowed,
                     DueDate = bor.DueDate,
                     Id = bor.Id,
                     MemberId = bor.MemberId,
                     MemberNo = bor.MemberNo,
                     RequestId = bor.RequestId,
                     Fees = bor.Fees,
                     UserId = bor.UserId
                 })
                .ToListAsync();
        }
    }
}
