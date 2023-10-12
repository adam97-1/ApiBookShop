using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public interface IBookRepository
    {
        public Book? AddBook(Book book);
        public Book? GetBook(int id);
        public IEnumerable<Book>? GetBooks();
        public void RemoveBook(int id);
        public Book? UpdateBook(int id, Book book);

    }
}
