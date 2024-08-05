using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Services
{
    public interface IBorrowService
    {
        Task<IEnumerable<Borrow>> GetBorrowsAsync();
        Task<Borrow> GetBorrowByIdAsync(int id);
        Task CreateBorrowAsync(Borrow borrow);
        Task UpdateBorrowAsync(Borrow borrow);
        Task DeleteBorrowAsync(int id);
        Task<bool> BorrowExistsAsync(int id);
    }
}