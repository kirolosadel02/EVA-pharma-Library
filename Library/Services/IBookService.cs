using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Author>> GetAuthorsAsync();
    }
}