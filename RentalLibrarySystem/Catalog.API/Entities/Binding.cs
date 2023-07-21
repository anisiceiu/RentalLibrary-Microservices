using System.Text.Json.Serialization;

namespace Catalog.API.Entities
{
    public class Binding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}
