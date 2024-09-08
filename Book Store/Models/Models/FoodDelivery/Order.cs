using Book_Store.Models.FoodDelivery;

namespace Book_Store.Models.FoodDelivery
{
    public class Order : BaseEntity
    {
        public String CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<ItemInOrder> ItemsInOrder { get; set; }
    }
}
