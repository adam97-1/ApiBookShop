using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiBookShop.Models;
using WebApiBookShop.Repositories;

namespace WebApiBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<CartData>> GetCarts()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            return _repository.GetCart(clientId).ToList();
        }

        [HttpGet("{cartId}")]
        [Authorize]
        public ActionResult<CartData> GetCart (int cartId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            return Ok(_repository.GetCart(clientId, cartId));
        }

        [HttpPost]
        [Authorize]
        public ActionResult<CartData> PutCart(CartData cart) {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            cart.Cart.ClientId = clientId;
            return Ok(_repository.AddCart(cart));

        }

        [HttpPut]
        [Authorize]
        public ActionResult<CartData> UpdateCart(int CartId, CartData cart)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            cart.Cart.ClientId = clientId;

            return Ok(_repository.UpdateCart(CartId, cart));
        }
        [HttpDelete]
        [Authorize]
        public IActionResult RemoveCart(int CartId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();

            var claims = identity.Claims;
            int clientId = Convert.ToInt32(claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);

            if (_repository.GetCart(clientId).Any(x => x.Cart.CartId == CartId))
                _repository.RemoveCart(CartId);
            return Ok();
        }

    }
}
