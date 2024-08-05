using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using Library.Models;
using Library.Services;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _bookService.GetBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["AuthorID"] = new SelectList(await _bookService.GetAuthorsAsync(), "AuthorID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,AuthorID")] Book book, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        book.Image = ms.ToArray();
                    }
                }

                try
                {
                    await _bookService.CreateBookAsync(book);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
            ViewData["AuthorID"] = new SelectList(await _bookService.GetAuthorsAsync(), "AuthorID", "Name", book.AuthorID);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _bookService.GetBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            ViewData["AuthorID"] = new SelectList(await _bookService.GetAuthorsAsync(), "AuthorID", "Name", book.AuthorID);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,AuthorID")] Book book, IFormFile? imageFile)
        {
            if (id != book.BookID) return NotFound();

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(ms);
                        book.Image = ms.ToArray();
                    }
                }

                try
                {
                    await _bookService.UpdateBookAsync(book);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
            ViewData["AuthorID"] = new SelectList(await _bookService.GetAuthorsAsync(), "AuthorID", "Name", book.AuthorID);
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _bookService.GetBookByIdAsync(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
