using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorID { get; set; }  // Primary Key, non-nullable

        [Required]
        public string Name { get; set; }  // Non-nullable

        public virtual ICollection<Book> Books { get; private set; }  // Collection of Books

        // Constructor to initialize non-nullable properties
        public Author(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Books = new List<Book>();  // Initialize collection to avoid null reference
        }

        // Parameterless constructor for EF Core
        public Author()
        {
            Name = string.Empty;
            Books = new List<Book>();  // Initialize collection to avoid null reference
        }
    }
}
