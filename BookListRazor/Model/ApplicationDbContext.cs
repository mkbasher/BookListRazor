using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Model
{
    public class ApplicationDbContext : DbContext   // Inherit DbContext Class
    {
        // to create constructor automatically : ctor + TAB +TAB
        // Parameter options is pussing to base for dependency injections 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Book> Book { get; set; } // Name Book is taken from Book.cs Class
    }
}
