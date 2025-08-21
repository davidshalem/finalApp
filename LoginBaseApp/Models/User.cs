using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginBaseApp.Models
{
    [SQLite.Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Indexed(Unique = true)] public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // דמו (בהמשך Hash)
    }
}
