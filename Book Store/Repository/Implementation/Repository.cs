using Book_Store.Models.Models;
using Book_Store.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T).IsAssignableFrom(typeof(Book)))
            {
                return entities
                    .Include("BookAuthors")
                    .Include("BookAuthors.Author")
                    .Include("PublisherBooks")
                    .Include("PublisherBooks.Publisher")
                    .AsEnumerable();
            }
            else if (typeof(T).IsAssignableFrom(typeof(Author)))
            {
                return entities
                    .Include("BookAuthors")
                    .Include("BookAuthors.Book")
                    .AsEnumerable();

            }

            else if (typeof(T).IsAssignableFrom(typeof(Publisher)))
            {
                return entities
                    .Include("PublisherBooks")
                    .Include("PublisherBooks.Book")
                    .Include("PublisherBooks.Book.BookAuthors")
                     .Include("PublisherBooks.Book.BookAuthors.Author")
                 
                      .AsEnumerable();

            }
            else if (typeof(T).IsAssignableFrom(typeof(Order)))
            {
                return entities
                    .Include("BookUser")
                    .Include("BooksInOrders")
                    .Include("BooksInOrders.Book")
              .AsEnumerable();

            }
            else
            {
                return entities.AsEnumerable();
            }
        }

        public T Get(int? id)
        {
            if (typeof(T).IsAssignableFrom(typeof(Author)))
            {
                return entities
                    .Include("BookAuthors")
                    .Include("BookAuthors.Book")
                    .First(s => s.Id == id);

            }
            else if (typeof(T).IsAssignableFrom(typeof(Book)))
            {
                return entities
                    .Include("BookAuthors")
                    .Include("BookAuthors.Author")
                    .Include("PublisherBooks")
                    .Include("PublisherBooks.Publisher")
                    .First(s => s.Id == id);

            }
            else if (typeof(T).IsAssignableFrom(typeof(Order)))
            {
                return entities
                    .Include("BookUser")
                    .Include("BooksInOrders")
                    .Include("BooksInOrders.Book")
                    .First(s => s.Id == id);

            }
            else if (typeof(T).IsAssignableFrom(typeof(Publisher)))
            {
                return entities
                    .Include("PublisherBooks")
                    .Include("PublisherBooks.Book")
                    .First(s => s.Id == id);

            }
            else
            {
                return entities.First(s => s.Id == id);
            }
           
        }
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        public List<T> InsertMany(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            entities.AddRange(entities);
            context.SaveChanges();
            return entities;
        }
        public List<T> RemoveMany(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
            return entities;
        }
    }

}
