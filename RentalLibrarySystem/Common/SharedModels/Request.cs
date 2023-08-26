namespace Common.SharedModels
{
    public class Request
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string RequestType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OperationName { get; set; }
    }
}
