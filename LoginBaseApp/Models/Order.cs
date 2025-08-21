using SQLite;

namespace LoginBaseApp.Models
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public int UserId { get; set; }       // קשר למשתמש
        public int ProductId { get; set; }    // קשר למוצר
        public int Quantity { get; set; } = 1;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
