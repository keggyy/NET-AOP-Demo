using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("BOOKS")]
    public class Book
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("TITLE")]
        public string Title { get; set; }

        [Column("AUTHOR")]
        public string Author { get; set; }
    }
}