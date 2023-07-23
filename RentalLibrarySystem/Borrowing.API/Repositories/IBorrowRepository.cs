using Borrowing.API.Entities;

namespace Borrowing.API.Repositories
{
    public interface IBorrowRepository
    {
        Task<Request> BookReserveRequest(Request request);
        Task<Request> BookRenewRequest(Request request);
        Task<List<Request>> GetAllBookRequest();
        Task<Borrow> IssueBook(int userId,Request request);
        Task<List<Borrow>> GetAllBorrowedBookByMemberId(int memberId);
        Task<List<Borrow>> GetAllOverDueBorrowedBook();
        Task<Return> ReturnBook(int userId, Borrow borrow);
        Task<Request?> GetRequestById(int requestId);
        Task<Borrow?> GetBorrowById(int borrowId);
        Task<Return?> GetReturnById(int returnId);

    }
}
