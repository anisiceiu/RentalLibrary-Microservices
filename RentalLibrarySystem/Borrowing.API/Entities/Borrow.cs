namespace Borrowing.API.Entities
{
    public class Borrow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }
        public int Fees { get; set; }
    }
}
