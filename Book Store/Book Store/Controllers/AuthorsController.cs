using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Store.Models.Models;
using Book_Store.Repository;
using Book_Store.Service.Implementation;
using Book_Store.Service.Interface;
using Book_Store.Models.DTO;

namespace Book_Store.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService AuthorService;
        private readonly IBookService bookService;
        private readonly ApplicationDbContext applicationDbContext;
        public AuthorsController(IAuthorService AuthorService, IBookService bookService)
        {
            this.AuthorService = AuthorService;
            this.bookService = bookService;
    
        }

        // GET: Authors
        public IActionResult Index()
        {
            return View(AuthorService.GetAuthors());
        }

        // GET: Authors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Author = AuthorService.GetAuthorById(id);
            if (Author == null)
            {
                return NotFound();
            }

            return View(Author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            var model = new Author(); 
            return View(model);
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Id")] Author Author)
        {
            if (ModelState.IsValid)
            {
                AuthorService.CreateNewAuthor(Author);
                return RedirectToAction(nameof(Index));
            }
            return View(Author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Author = AuthorService.GetAuthorById(id);
            if (Author == null)
            {
                return NotFound();
            }
            return View(Author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,Description,Id")] Author Author)
        {
            if (id != Author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AuthorService.UpdateAuthor(Author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(Author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Author);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int id)
        {
            var Author = AuthorService.GetAuthorById(id);
            if (Author != null)
            {
                AuthorService.DeleteAuthor(id);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Authors/Delete/5
    

        private bool AuthorExists(int id)
        {
            if (AuthorService.GetAuthorById(id) != null)
            {
                return true;
            }
            return false;
        }
        [HttpGet]
        public IActionResult AddBook(int id)
        {   
            AuthorService.GetAuthorById(id);
            BookAuthorDTO AuthorBooks = new BookAuthorDTO();
            AuthorBooks.AuthorId = id;
            return View(AuthorBooks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook([Bind("Price,Title,Description,AuthorId,ImageUrl")] BookAuthorDTO BookAuthor)
        {
            Book book = new Book();
            if (ModelState.IsValid)
            {
               
                book.Title=BookAuthor.Title;
                book.Description=BookAuthor.Description;
                book.Price=BookAuthor.Price;
                book.ImageUrl=BookAuthor.ImageUrl;
                Book CreatedBook=  bookService.CreateNewBook(book);
                Author author = AuthorService.GetAuthorById(BookAuthor.AuthorId);
                BookAuthor bookAuthor = new BookAuthor
                {
                    AuthorId = BookAuthor.AuthorId,
                    BookId = CreatedBook.Id
                };

                AuthorService.CreateBook(bookAuthor);

                return RedirectToAction("Details",new {id =bookAuthor.AuthorId });
            }
            return View(book);
        }

        public async Task<IActionResult> EditBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBook(int id, [Bind("Price,Title,Description,Id,ImageUrl")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookService.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        public IActionResult DeleteBook(int id)
        {
            var Author = AuthorService.DeleteBook(id);
            return RedirectToAction(nameof(Details), new { id = Author.Id });
        }

     
    }

}