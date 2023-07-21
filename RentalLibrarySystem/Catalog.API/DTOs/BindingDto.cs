using System.ComponentModel.DataAnnotations;

namespace Catalog.API.DTOs
{
    public class BindingDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
