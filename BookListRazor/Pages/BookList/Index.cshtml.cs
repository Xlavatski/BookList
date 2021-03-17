using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id) 
        {
            var book = await _db.books.FindAsync(id);
            if (book == null) 
            {
                return NotFound();
            }

            _db.books.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");

        }
    }
}
