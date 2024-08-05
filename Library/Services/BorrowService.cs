using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Services;
namespace Library.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly LibraryContext _context;

        public BorrowService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrow>> GetBorrowsAsync()
        {
            return await _context.Borrows.Include(b => b.Book).Include(b => b.Member).ToListAsync();
        }

        public async Task<Borrow> GetBorrowByIdAsync(int id)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(b => b.BorrowID == id);
        }

        public async Task CreateBorrowAsync(Borrow borrow)
        {
            _context.Add(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBorrowAsync(Borrow borrow)
        {
            _context.Update(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBorrowAsync(int id)
        {
            var borrow = await _context.Borrows.FindAsync(id);
            if (borrow != null)
            {
                _context.Borrows.Remove(borrow);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BorrowExistsAsync(int id)
        {
            return await _context.Borrows.AnyAsync(b => b.BorrowID == id);
        }
    }
}