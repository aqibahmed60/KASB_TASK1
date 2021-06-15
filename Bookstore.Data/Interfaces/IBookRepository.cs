using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBook();

        Book GetBook(int id);

        bool AddNewBook(Book book);

        bool Remove(int id);

        List<Book> UpdateBook(int id, Book Book);


    }
}
