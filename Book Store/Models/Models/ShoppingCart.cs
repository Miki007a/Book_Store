using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.Models
{
    public class ShoppingCart : BaseEntity
    {

         
            public string? BookUserId { get; set; }
            public BookUser? BookUser { get; set; }
            public virtual List<BooksInShoppingCart>? Books { get; set; }
            public ShoppingCart() { 
            Books = new List<BooksInShoppingCart>();
            }

    }
}
