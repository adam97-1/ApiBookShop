using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;

namespace WebApiBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Client> PostClient(Client client)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                client.Role = "User";
                return _repository.AddClient(client);
                } 
                var claims = identity.Claims;
                string rola = claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;
                if (rola != "Admin")
                    client.Role = "User";
                return _repository.AddClient(client);
            }
            catch (Exception ex) 
            {
                client.Role = "User";
                return _repository.AddClient(client);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Client>>  GetClients()
        {
            var client = _repository.GetClients();
            if (client == null)
                return NotFound();
            return client.ToList();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _repository.GetClient(id);
            if (client == null)
                return NotFound();
            return client;

        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteClient(int id) 
        {
            _repository.RemoveClient(id);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public ActionResult<Client> PutClient(int id, Client client) 
        {
            var c = _repository.UpdateClient(id, client);
            if (c == null)
                return BadRequest();
            return c;
        }
    }
}
