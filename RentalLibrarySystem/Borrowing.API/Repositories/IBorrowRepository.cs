using Borrowing.API.Entities;

namespace Borrowing.API.Repositories
{
    public interface IBorrowRepository
    {
        Task<Request> BookReserveRequestAsync(Request request);
        Task<Request> BookRenewRequestAsync(Request request);
        Task<List<Request>> GetAllBookRequestAsync();
        Task<Borrow> IssueBookAsync(int userId,Request request);
        Task<List<Borrow>> GetAllBorrowedBookByMemberIdAsync(int memberId);
        Task<List<Borrow>> GetAllOverDueBorrowedBookAsync();
        Task<Return> ReturnBookAsync(int userId, Borrow borrow);
        Task<Request?> GetRequestByIdAsync(int requestId);
        Task<Borrow?> GetBorrowByIdAsync(int borrowId);
        Task<Return?> GetReturnByIdAsync(int returnId);
        Task<int> CalculateFineAsync(DateTime dueDate);
    }
}
