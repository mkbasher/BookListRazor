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
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Books { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Books = new Book();
            //create
            if(id==null)
            {
                return Page();
            }

            // For update
            Books = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if(Books==null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Books.Id==0)
                {
                    _db.Book.Add(Books);
                }
                else
                {
                    _db.Book.Update(Books); //this will update all fields.

                    // For updating specific fields use below:
                /*  var BookFromDb = await _db.Book.FindAsync(Books.Id);
                    BookFromDb.Name = Books.Name;
                    BookFromDb.Author = Books.Author;
                    BookFromDb.ISBN = Books.ISBN;*/
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
