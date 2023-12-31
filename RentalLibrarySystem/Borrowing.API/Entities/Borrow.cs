﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Borrowing.API.Entities
{
    public class Borrow
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MemberId { get; set; }
        public string? MemberNo { get; set; }
        public int BookId { get; set; }
        public int RequestId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }
        public int Fees { get; set; }
        public bool IsReturned { get; set; }
        [NotMapped]
        public string BookName { get; set; }
    }
}
