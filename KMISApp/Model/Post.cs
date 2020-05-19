using SQLite;

namespace KMISApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(25)]
        public string Service { get; set; }
    }
}
