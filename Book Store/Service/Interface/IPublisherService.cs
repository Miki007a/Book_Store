using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Interface
{
    public interface IPublisherService
    {
        public List<Publisher> GetPublishers();
        public Publisher GetPublisherById(int? id);
        public Publisher CreateNewPublisher(Publisher Publisher);
        public Publisher UpdatePublisher(Publisher Publisher);
        public Publisher DeletePublisher(int id);
        public PublisherBooks PublishBook(PublisherBooks publisherBooks);

    }
}
