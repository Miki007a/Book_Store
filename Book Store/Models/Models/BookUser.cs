
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.Models
{
   public class BookUser : IdentityUser
    {
     

        public ShoppingCart ShoppingCart { get; set; }

        public List<BooksInOrder> Orders { get; set; }

        public BookUser() {
            Orders = new List<BooksInOrder>(); 
        }
    }
}
