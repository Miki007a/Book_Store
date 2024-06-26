using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.Models
{
    public class Publisher:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<PublisherBooks>? PublisherBooks { get; set; }
        public Publisher()
        {
           PublisherBooks = new List<PublisherBooks>();
        }

    }
}
