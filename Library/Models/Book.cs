using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }  // Primary Key, non-nullable

        [Required]
        public string? Title { get; set; }  // Non-nullable

        [Required]
        public int? AuthorID { get; set; }  // Non-nullable foreign key

        public virtual Author? Author { get; set; }  // Navigation property, will be set by EF Core

        public virtual ICollection<Borrow> Borrows { get; set; }  // Collection of Borrows, initialized in constructor

        // Parameterless constructor for EF Core and model binding
        public Book()
        {
            Borrows = new List<Borrow>();  // Initialize collection to avoid null reference
        }

        // Constructor to initialize non-nullable properties
        public Book(string title, int authorID)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            AuthorID = authorID;
            Borrows = new List<Borrow>();  // Initialize collection to avoid null reference
        }
    }
}
