using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext _context;

        public BookRepository(BookShopContext context)
        {
            _context = context;
        }
        public Book? AddBook(Book book)
        {
            if (book == null)
                return null;
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book? GetBook(int id)
        {
            var book = _context.Books.Find(id) ;
            if (book == null)
                return null;
            return book;
        }

        public IEnumerable<Book>? GetBooks()
        {
            return _context.Books.ToList();
        }

        public void RemoveBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return;
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public Book? UpdateBook(int id, Book book)
        {
            if(id != book.BookId)
                return null;
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }
    }
}
