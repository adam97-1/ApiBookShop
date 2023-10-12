using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Packaging;
using System.Collections.Generic;
using System.Net;
using WebApiBookShop.Controllers;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;
using static System.Reflection.Metadata.BlobBuilder;

namespace UnitTestBookShop
{
    [TestClass]
    public class UnitTestBookController
    {
        [TestMethod]
        public void GetBooks()
        {
            //Arrange

            List<Book> books = new List<Book>();
            Book book = new Book();
            book.BookId = 1;
            book.Price = 10;
            book.Author = "J.R.R. Tolkien";
            book.Quantity = 10;
            book.Title = "W�adca Pier�cieni: Dru�yna Pier�cienia";
            book.Description = "Podr� hobbita z Shire i jego o�miu towarzyszy, kt�rej celem jest zniszczenie pot�nego pier�cienia po��danego przez Czarnego W�adc� - Saurona.";
            books.Add(book);

            book.BookId = 2;
            book.Price = 15;
            book.Author = "J.R.R. Tolkien";
            book.Quantity = 12;
            book.Title = "W�adca Pier�cieni: Dwie wie�e";
            book.Description = "Dru�yna Pier�cienia zostaje rozbita, lecz zdesperowany Frodo za wszelk� cen� chce wype�ni� powierzone mu zadanie. Aragorn z towarzyszami przygotowuje si�, by odeprze� atak hord Sarumana.";
            books.Add(book);


            var mock = new Mock<IBookRepository>();
            mock.Setup(mock => mock.GetBooks()).Returns(books);
            BookController controller = new BookController(mock.Object);
            //Act
            var result = controller.GetBooks();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Book>>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value.Count(), books.Count);
            Assert.AreEqual(result.Value.ElementAt(0), books.ElementAt(0));
            Assert.AreEqual(result.Value.ElementAt(1), books.ElementAt(1));
        }

        [TestMethod]
        public void GetBook()
        {
            //Arrange

            Book book = new Book();
            book.BookId = 1;
            book.Price = 10;
            book.Author = "J.R.R. Tolkien";
            book.Quantity = 10;
            book.Title = "W�adca Pier�cieni: Dru�yna Pier�cienia";
            book.Description = "Podr� hobbita z Shire i jego o�miu towarzyszy, kt�rej celem jest zniszczenie pot�nego pier�cienia po��danego przez Czarnego W�adc� - Saurona.";

            var mock = new Mock<IBookRepository>();
            mock.Setup(m => m.GetBook(book.BookId)).Returns(book);
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.GetBook(book.BookId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Book>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value, book);
        }
        [TestMethod]
        public void AddBook()
        {
            //Arrange
            Book book = new Book();
            book.BookId = 1;
            book.Price = 10;
            book.Author = "J.R.R. Tolkien";
            book.Quantity = 10;
            book.Title = "W�adca Pier�cieni: Dru�yna Pier�cienia";
            book.Description = "Podr� hobbita z Shire i jego o�miu towarzyszy, kt�rej celem jest zniszczenie pot�nego pier�cienia po��danego przez Czarnego W�adc� - Saurona.";

            var mock = new Mock<IBookRepository>();
            mock.Setup(m => m.AddBook(book)).Returns(book);
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.PostBook(book);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Book>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value, book);

        }
        [TestMethod]
        public void UpdateBook()
        {
            //Arrange
            Book book = new Book();
            book.BookId = 1;
            book.Price = 10;
            book.Author = "J.R.R. Tolkien";
            book.Quantity = 10;
            book.Title = "W�adca Pier�cieni: Dru�yna Pier�cienia";
            book.Description = "Podr� hobbita z Shire i jego o�miu towarzyszy, kt�rej celem jest zniszczenie pot�nego pier�cienia po��danego przez Czarnego W�adc� - Saurona.";

            var mock = new Mock<IBookRepository>();
            mock.Setup(m => m.UpdateBook(book.BookId, book)).Returns(book);
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.PutBook(book.BookId, book);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Book>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value, book);
        }
    }
}