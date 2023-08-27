namespace Identity.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public int? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? MemberNo { get; set; }
    }
}
