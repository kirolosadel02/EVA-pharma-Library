using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Member> Members { get; set; } = default!;
        public DbSet<Borrow> Borrows { get; set; } = default!;

        public LibraryContext() { }

        // Constructor that takes DbContextOptions
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Define the connection string (replace with your actual connection string)
            optionsBuilder.UseSqlServer("Server=DESKTOP-GBUG1I8\\SQLEXPRESS;Database=Library;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Author and Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID);

            // Configure the many-to-many relationship between Member and Book via Borrow
            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Borrows)
                .HasForeignKey(b => b.MemberID);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrows)
                .HasForeignKey(b => b.BookID);

            // Define primary keys
            modelBuilder.Entity<Member>()
                .HasKey(m => m.ID);

            modelBuilder.Entity<Book>()
                .HasKey(b => b.BookID);

            modelBuilder.Entity<Author>()
                .HasKey(a => a.AuthorID);

            modelBuilder.Entity<Borrow>()
                .HasKey(b => b.BorrowID);
        }
    }
}
