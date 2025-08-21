using SQLite;

namespace LoginBaseApp.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Indexed] public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
