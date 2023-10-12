using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiBookShop.Controllers;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;
using static System.Reflection.Metadata.BlobBuilder;

namespace UnitTestBookShop
{
    [TestClass]
    public class UnitTestClientController
    {
        [TestMethod]
        public void GetClients()
        {
            //Arrange
            List<Client> clients = new();
            Client client = new();
            client.ClientId = 1;
            client.Adress = "Warszawa ul. Prosta 35";
            client.Email = "jankowalski@gmail.com";
            client.Name = "Jan";
            client.Surname = "Kowalski";
            client.Login = "kowal13";
            client.Password = "m34!mn#f";
            clients.Add(client);

            client.ClientId = 2;
            client.Adress = "Warszawa ul. Prosta 36";
            client.Email = "kotek87@gmail.com";
            client.Name = "Tomasz";
            client.Surname = "Kot";
            client.Login = "kotek87";
            client.Password = "ju*^53Gt^";
            clients.Add(client);


            var mock =  new Mock<IClientRepository>();
            mock.Setup(m => m.GetClients()).Returns(clients);
            var controller = new ClientController(mock.Object);
            //Act
            var result = controller.GetClients();
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Client>>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value.Count(), clients.Count);
            Assert.AreEqual(result.Value.ElementAt(0), clients.ElementAt(0));
            Assert.AreEqual(result.Value.ElementAt(1), clients.ElementAt(1));

        }

        [TestMethod]
        public void GetBook()
        {
            //Arrange

            Client client = new();
            client.ClientId = 1;
            client.Adress = "Warszawa ul. Prosta 35";
            client.Email = "jankowalski@gmail.com";
            client.Name = "Jan";
            client.Surname = "Kowalski";
            client.Login = "kowal13";
            client.Password = "m34!mn#f";
            client.Role = "User";

            var mock = new Mock<IClientRepository>();
            mock.Setup(m => m.GetClient(client.ClientId)).Returns(client);
            var controller = new ClientController(mock.Object);
            //Act
            var result = controller.GetClient(client.ClientId);
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value, client);
        }

        [TestMethod]
        public void AddBook()
        {
            Client client = new();
            client.ClientId = 1;
            client.Adress = "Warszawa ul. Prosta 35";
            client.Email = "jankowalski@gmail.com";
            client.Name = "Jan";
            client.Surname = "Kowalski";
            client.Login = "kowal13";
            client.Password = "m34!mn#f";
            client.Role = "User";

            var mock = new Mock<IClientRepository>();
            mock.Setup(m => m.AddClient(client)).Returns(client);
            var controller = new ClientController(mock.Object);
            //Act
            var result = controller.PostClient(client);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value, client);
        }
        [TestMethod]
        public void UpdateBook()
        {
            //Arrange
            Client client = new();
            client.ClientId = 1;
            client.Adress = "Warszawa ul. Prosta 35";
            client.Email = "jankowalski@gmail.com";
            client.Name = "Jan";
            client.Surname = "Kowalski";
            client.Login = "kowal13";
            client.Password = "m34!mn#f";
            client.Role = "User";

            var mock = new Mock<IClientRepository>();
            mock.Setup(m => m.UpdateClient(client.ClientId, client)).Returns(client);
            var controller = new ClientController(mock.Object);
            //Act
            var result = controller.PutClient(client.ClientId, client);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Client>));
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value, client);
        }
    }
}
