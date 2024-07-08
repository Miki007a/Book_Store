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
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _repository;
        private readonly IRepository<PublisherBooks> _PublishedBooksRepository;
        private readonly IRepository<Book> _BookRepository;

        public PublisherService(IRepository<Publisher> repository, IRepository<Book> bookRepository,IRepository<PublisherBooks> repository1)
        {
            _repository = repository;
            _BookRepository = bookRepository;
            _PublishedBooksRepository = repository1;
        }
        public Publisher CreateNewPublisher(Publisher Publisher)
        {

            return _repository.Insert(Publisher);

        }

        public Publisher DeletePublisher(int id)
        {
            var Publisher = _repository.Get(id);
            return _repository.Delete(Publisher);
        }

        public Publisher GetPublisherById(int? id)
        {
            return _repository.Get(id);
        }

        public List<Publisher> GetPublishers()
        {
            return _repository.GetAll().ToList();
        }

        public PublisherBooks PublishBook(PublisherBooks publisherBooks)
        {

            Publisher publisher = _repository.Get(publisherBooks.PublisherId);
            publisherBooks.Book=_BookRepository.Get(publisherBooks.BookId);
            publisherBooks.Publisher= publisher;
            publisher.PublisherBooks.Add(publisherBooks);
           return _PublishedBooksRepository.Insert(publisherBooks);
        }

        public Publisher UnpublishBook(int id)
        {
            var publisherBook = _PublishedBooksRepository.Get(id);
            var publisher = _repository.Get(publisherBook.PublisherId);
            publisher.PublisherBooks.Remove(publisherBook);
            return _repository.Update(publisher);
         
        }

        public Publisher UpdatePublisher(Publisher Publisher)
        {
            return _repository.Update(Publisher);
        }
    }
}

