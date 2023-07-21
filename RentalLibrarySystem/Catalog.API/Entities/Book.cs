using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
        public string Language { get; set; }
        public int NoOfCopies { get; set; }
        public int NoOfAvailableCopies { get; set; }
        public int BindingId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Binding Binding { get; set; }
        public string? ThumbnailImageUrl { get; set; }
    }
}
