using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Interface
{
    public interface IAuthorService
    {
        public List<Author> GetAuthors();
        public Author GetAuthorById(int? id);
        public Author CreateNewAuthor(Author Author);
        public Author UpdateAuthor(Author Author);
        public BookAuthor CreateBook(BookAuthor bookAuthor);
        public Author DeleteAuthor(int id);
        public Author DeleteBook(int id);
       
    }
}
