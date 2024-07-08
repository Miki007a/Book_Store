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
        private readonly IRepository<Order> repository;
        public OrderService(IRepository<Order> orderRepository)
        {
            repository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return repository.GetAll().ToList();
        }

        public Order GetDetailsForOrder(int id)
        {
            return repository.Get(id);
        }
    }
}
