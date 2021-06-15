using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Models;
using Microsoft.EntityFrameworkCore; 

namespace Bookstore.Data.Models
{
    public class BookStoreContext : DbContext
    {

        public BookStoreContext (DbContextOptions<BookStoreContext> options)
            :base (options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}
