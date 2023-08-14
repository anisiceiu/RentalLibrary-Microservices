namespace Common.SharedModels
{
    public class CurrentUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? MemberId { get; set; }
        public string MemberName { get; set; }
    }
}
