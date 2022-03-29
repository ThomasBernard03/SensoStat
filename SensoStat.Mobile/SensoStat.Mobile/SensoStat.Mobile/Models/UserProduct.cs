using SQLite;

namespace SensoStat.Mobile.Models
{
    [Table("UserProduct")]
    public class UserProduct
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Position { get; set; }

        public Survey Survey { get; set; }
    }
}