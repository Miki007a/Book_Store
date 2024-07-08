using Book_Store.Repository.Interface;
using Book_Store.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Store.Models.Models;

namespace Book_Store.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<BookUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<BookUser>();
        }
        public IEnumerable<BookUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public BookUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include("Orders")
                .Include("Orders.Book")
                .Include("ShoppingCart")
                .Include("ShoppingCart.Books")
                .Include("ShoppingCart.Books.Book")
                .First(s => s.Id == strGuid);
        }
        public void Insert(BookUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(BookUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(BookUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }

}
