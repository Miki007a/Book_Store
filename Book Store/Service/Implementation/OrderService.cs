using Book_Store.Models.Models;
using Book_Store.Repository.Interface;
using Book_Store.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;   
        private readonly IUserRepository _userRepository;

        public OrderService(IRepository<Order> repository,IUserRepository userRepository)
        {
            this._orderRepository = repository;
            this._userRepository = userRepository;
        }

        public Order ConfirmOrder(Order order)
        {
            return _orderRepository.Delete(order);
        }

        public Order GetOrder(int id)
        {
            return _orderRepository.Get(id);
        }

        public List<Order> GetOrdersForUser(string? userId)
        {

            var loggedInUser = _userRepository.Get(userId);
            return loggedInUser.Orders.ToList();
        }
    }
}
