using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.DTO
{
    public class AddAuthorBookToPublisherDTO
    {
        public Publisher Publisher { get; set; }
        public List<Author> Authors { get; set; }
    }
}
