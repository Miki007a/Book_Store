using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.DTO
{
    public class BookInOrderDTO
    {

        public int Quantity { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
