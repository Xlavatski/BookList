using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            return Json(new {data = await _db.books.ToListAsync() });
        }

        [HttpDelete]

        public async Task <IActionResult> Delete(int id) 
        {
            var bookFormDb = await _db.books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFormDb == null) 
            {
                return Json(new { success = false, message = "Error while Deleting"});
            }
            _db.books.Remove(bookFormDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" }) ;
        }
    }
}
