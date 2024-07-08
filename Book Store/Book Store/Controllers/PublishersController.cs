using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Store.Models.Models;
using Book_Store.Repository;
using Book_Store.Service.Interface;
using Book_Store.Service.Implementation;
using Book_Store.Models.DTO;
using System.Security.Policy;
using Publisher = Book_Store.Models.Models.Publisher;

namespace Book_Store.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public PublishersController(IPublisherService publisherService,IBookService bookService,IAuthorService author)
        {
            this.publisherService = publisherService;
            this.bookService = bookService;
            this.authorService = author;
        }

        // GET: Publishers
        public IActionResult Index()
        {
            return View(publisherService.GetPublishers());
        }

        // GET: Publishers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            List<Author> authors = authorService.GetAuthors().ToList();
            AddAuthorBookToPublisherDTO addAuthorBookToPublisherDTO = new AddAuthorBookToPublisherDTO
            {
                Publisher = publisher,
                Authors = authors
            };

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            var model = new Publisher();
            return View(model);
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Id")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                publisherService.CreateNewPublisher(publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Name,Description,Id")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   publisherService.UpdatePublisher(publisher);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.Id))
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
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public IActionResult Delete(int id)
        {
            var publisher = publisherService.GetPublisherById(id);
            if (publisher != null)
            {
                publisherService.DeletePublisher(id);
            }

            return RedirectToAction("Index");
        }

     

        private bool PublisherExists(int id)
        {
           if (publisherService.GetPublisherById(id)!=null)
            {
                return true;
            }
            return false;
        }

        public IActionResult PublishBook(int id)
        {
           Publisher publisher=publisherService.GetPublisherById(id);
            
          
           
            List<Author> authors = authorService.GetAuthors().ToList();
            AddAuthorBookToPublisherDTO addAuthorBookToPublisherDTO = new AddAuthorBookToPublisherDTO
            {
                Publisher = publisher,
                Authors = authors
            };
            return View(addAuthorBookToPublisherDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PublishBook([Bind("PublisherId,BookId")] PublisherBooks publisherBooks)
        {
            if (ModelState.IsValid)
            {
                publisherService.PublishBook(publisherBooks);

                return RedirectToAction("Details",new {id=publisherBooks.PublisherId});
            }
            return View(publisherBooks);
        }


        public IActionResult DeleteBook(int id)
        {
            
              var publisher=  publisherService.UnpublishBook(id);
            


            return RedirectToAction(nameof(Details),new {id = publisher.Id});
        }

   
    
    }
}
