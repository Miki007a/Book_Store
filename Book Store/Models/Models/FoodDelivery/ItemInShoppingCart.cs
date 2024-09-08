using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.FoodDelivery
{
    public class ItemInShoppingCart : BaseEntity
    {

        public Guid ItemId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public Item? Item { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
