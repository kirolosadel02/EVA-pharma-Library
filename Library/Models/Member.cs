using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Primary Key

        [Required]
        public string? Name { get; set; } // Updated to non-nullable

        // Navigation property
        public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

        // Constructor
        public Member(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            ID = id;
            Name = name;
        }

        // Parameterless constructor for Entity Framework
        public Member() { }
    }
}
