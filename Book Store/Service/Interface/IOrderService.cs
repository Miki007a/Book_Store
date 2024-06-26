using Book_Store.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> GetOrdersForUser(string? userId);

        public Order GetOrder(int id);

        public Order ConfirmOrder(Order order);
    }
}
