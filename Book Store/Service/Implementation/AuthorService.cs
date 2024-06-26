using Book_Store.Models.Models;
using Book_Store.Repository.Interface;
using Book_Store.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Implementation
{
    public class AuthorService:IAuthorService
    {
        private readonly IRepository<Author> _repository;

        private readonly IRepository<BookAuthor> _bookAuthorRepository;

        private readonly IRepository<Book> repositoryBook;

        public AuthorService(IRepository<Author> repository, IRepository<BookAuthor> bookAuthorRepository, IRepository<Book> repositoryBook)
        {
            _repository = repository;
            _bookAuthorRepository = bookAuthorRepository;
            this.repositoryBook = repositoryBook;
        }

        public BookAuthor CreateBook(BookAuthor bookAuthor)

        {
            Author author = _repository.Get(bookAuthor.AuthorId);
            bookAuthor.Author = author;
            bookAuthor.Book=repositoryBook.Get(bookAuthor.BookId);
            author.BookAuthors.Add(bookAuthor);
            return _bookAuthorRepository.Insert(bookAuthor);
        }

        public Author CreateNewAuthor(Author Author)
        {

            return _repository.Insert(Author);

        }

        public Author DeleteAuthor(int id)
        {
            var Author = _repository.Get(id);
            return _repository.Delete(Author);
        }

        public Author GetAuthorById(int? id)
        {
            return _repository.Get(id);
        }

        public List<Author> GetAuthors()
        {
            return _repository.GetAll().ToList();
        }

        public Author UpdateAuthor(Author Author)
        {
            return _repository.Update(Author);
        }
    }
}

