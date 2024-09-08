using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Models.FoodDelivery
{
    public class ItemInRestaurant : BaseEntity
    {
    
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
