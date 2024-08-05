using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int? AuthorID { get; set; }

        public virtual Author? Author { get; set; }

        public virtual ICollection<Borrow> Borrows { get; set; }

        // New property to store the image
        public byte[]? Image { get; set; }

        public Book()
        {
            Borrows = new List<Borrow>();
        }

        public Book(string title, int authorID)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            AuthorID = authorID;
            Borrows = new List<Borrow>();
        }
    }
}
