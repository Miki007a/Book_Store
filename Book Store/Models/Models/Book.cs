namespace Book_Store.Models.Models
{
    public class Book : BaseEntity
    {
        public int Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<BookAuthor>? BookAuthors { get; set; }
        public List<BooksInOrder>? BooksInOrders { get; set; }
        public List<PublisherBooks>? PublisherBooks { get; set; }

        public List<BooksInShoppingCart> BooksInShoppingCarts { get; set; }

        public string ImageUrl { get; set; }
        public Book() {
            BookAuthors = new List<BookAuthor>();
            BooksInOrders = new List<BooksInOrder>();
            PublisherBooks = new List<PublisherBooks>();
            BooksInShoppingCarts = new List<BooksInShoppingCart>();

        }

    }
}
