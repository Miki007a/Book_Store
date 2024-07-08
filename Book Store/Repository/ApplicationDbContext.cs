using Book_Store.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Book_Store.Repository
{
    public class ApplicationDbContext : IdentityDbContext<BookUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<BooksInOrder> BooksInOrders { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<PublisherBooks> PublishersBooks { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<BooksInShoppingCart> BooksInShoppingCarts { get;set; }

    }
}
