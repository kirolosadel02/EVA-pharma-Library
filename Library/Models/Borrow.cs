using System;
using Library.Models;

namespace Library.Models
{
    public class Borrow
    {
        public int BorrowID { get; set; }  // Primary Key
        public int MemberID { get; set; }  // Foreign Key for Member
        public int BookID { get; set; }  // Foreign Key for Book
        public DateTime BorrowDate { get; set; }  // Date when the book was borrowed

        public virtual Member? Member { get; set; }  // Navigation Property
        public virtual Book? Book { get; set; }  // Navigation Property

        // Parameterless constructor for EF Core
        public Borrow()
        {
        }

        // Constructor to initialize properties
        public Borrow(int borrowId, int memberId, int bookId, DateTime borrowDate, Member member, Book book)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            BorrowID = borrowId;
            MemberID = memberId;
            BookID = bookId;
            BorrowDate = borrowDate;
            Member = member;
            Book = book;
        }
    }
}
