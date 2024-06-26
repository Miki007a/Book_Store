using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.Models
{
    public class PublisherBooks :BaseEntity
    {
        public int BookId { get; set; }

        public Book? Book { get; set; }

        public int PublisherId { get; set; }

        public Publisher? Publisher { get; set; }

    }
}
