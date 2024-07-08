using Book_Store.Models.DTO;
using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Interface
{
    public interface IBookService
    { 
        public List<Book> GetBooks();
        public Book GetBookById(int? id);
        public Book CreateNewBook(Book book);
        public Book UpdateBook(Book book);
        public Book DeleteBook(int id);
     
    }
}

