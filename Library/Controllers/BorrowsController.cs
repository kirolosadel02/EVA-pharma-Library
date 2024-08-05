using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Models;
using Library.Services;

namespace Library.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService; // Assuming you have a similar service for books
        private readonly IMemberService _memberService; // Assuming you have a similar service for members

        public BorrowsController(IBorrowService borrowService, IBookService bookService, IMemberService memberService)
        {
            _borrowService = borrowService;
            _bookService = bookService;
            _memberService = memberService;
        }

        // GET: Borrows
        public async Task<IActionResult> Index()
        {
            var borrows = await _borrowService.GetBorrowsAsync();
            return View(borrows);
        }

        // GET: Borrows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _borrowService.GetBorrowByIdAsync(id.Value);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // GET: Borrows/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BookID"] = new SelectList(await _bookService.GetBooksAsync(), "BookID", "Title");
            ViewData["MemberID"] = new SelectList(await _memberService.GetMembersAsync(), "ID", "ID");
            return View();
        }

        // POST: Borrows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowID,MemberID,BookID,BorrowDate")] Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                await _borrowService.CreateBorrowAsync(borrow);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(await _bookService.GetBooksAsync(), "BookID", "Title", borrow.BookID);
            ViewData["MemberID"] = new SelectList(await _memberService.GetMembersAsync(), "ID", "ID", borrow.MemberID);
            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _borrowService.GetBorrowByIdAsync(id.Value);
            if (borrow == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(await _bookService.GetBooksAsync(), "BookID", "Title", borrow.BookID);
            ViewData["MemberID"] = new SelectList(await _memberService.GetMembersAsync(), "ID", "ID", borrow.MemberID);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowID,MemberID,BookID,BorrowDate")] Borrow borrow)
        {
            if (id != borrow.BorrowID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _borrowService.UpdateBorrowAsync(borrow);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!await _borrowService.BorrowExistsAsync(borrow.BorrowID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["BookID"] = new SelectList(await _bookService.GetBooksAsync(), "BookID", "Title", borrow.BookID);
            ViewData["MemberID"] = new SelectList(await _memberService.GetMembersAsync(), "ID", "ID", borrow.MemberID);
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await _borrowService.GetBorrowByIdAsync(id.Value);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _borrowService.DeleteBorrowAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}