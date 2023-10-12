using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;

namespace WebApiBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<OrderData>> GetOrders()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            return Ok(_repository.GetOrders(clientId));
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public ActionResult<OrderData> GetOrder( int orderId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            return Ok(_repository.GetOrder(orderId, clientId));
        }

        [HttpPost]
        [Authorize]
        public ActionResult<OrderData> PutOrder(OrderData order)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            order.Order.ClientId = clientId;

            return Ok(_repository.AddOrder(order));

        }

        [HttpPut]
        [Authorize]
        public ActionResult<OrderData> UpdateCart(int CartId, OrderData order)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            order.Order.ClientId = clientId;

            return Ok(_repository.UpdateOrder(CartId, order));
        }
        [HttpDelete]

        public IActionResult RemoveCart(int OrderId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            if (_repository.GetOrders(clientId).Any(x => x.Order.OrderId == OrderId))
                _repository.RemoveOrder(OrderId);
            return Ok();

        }
    }
}
