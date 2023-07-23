namespace Borrowing.API.Entities
{
    public class Return
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BorrowId { get; set; }
        public Borrow Borrow { get; set; }
        public DateTime DateReturned { get; set; }
        public DateTime DueDate { get; set; }
        public int Fine { get; set; }

    }
}
