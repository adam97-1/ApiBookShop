using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;

namespace WebApiBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repositiory;

        public BookController(IBookRepository repository)
        {
            _repositiory = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var book = _repositiory.GetBooks();
            if (book == null)
                return NotFound();
            return book.ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _repositiory.GetBook(id);
            if (book == null)
                return NotFound();
            return book;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book> PostBook(Book book)
        {
            return _repositiory.AddBook(book);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book> PutBook(int id, Book book)
        {
            var b = _repositiory.UpdateBook(id, book);
            if (book == null)
                return BadRequest();
            return b;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            _repositiory.RemoveBook(id);
            return Ok();
        }
    }
}
