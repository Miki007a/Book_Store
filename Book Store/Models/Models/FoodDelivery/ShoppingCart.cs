
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.FoodDelivery
{
    public class ShoppingCart : BaseEntity
    {
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public virtual ICollection<ItemInShoppingCart>? ItemInShoppingCarts { get; set; }

    }
}
