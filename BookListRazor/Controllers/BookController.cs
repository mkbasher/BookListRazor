using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{   [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Add this API in pipeline & Middleware (Starup.cs) .. 
            //services.AddControllersWithViews(); this is pipeline
            //endpoints.MapControllers(); this middleware
            return Json(new { data = await _db.Book.ToListAsync() }); 
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteBook(int bId)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(bk => bk.Id == bId);
            if(bookFromDb==null)
            {
                return Json(new { success = false, message="Error while deleting" });
            }
            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = "true", message = "Delete successful" });
        }
    }
}
