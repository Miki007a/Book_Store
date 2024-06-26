using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<BookAuthor>? BookAuthors { get; set; }
        public Author()
        {
            BookAuthors = new List<BookAuthor>();
        }

    }
}
