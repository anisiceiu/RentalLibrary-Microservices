using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.API.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
        public string Language { get; set; }
        [Required]
        public int NoOfCopies { get; set; }
        public int NoOfAvailableCopies { get; set; }
        [Required]
        public int BindingId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? BindingName { get; set; }
        public string? ThumbnailImageUrl { get; set; }
        [Required]
        public IFormFile  FormFile { get; set; }
    }
}
