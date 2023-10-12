using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public interface ICartRepository
    {
        public IEnumerable<CartData> GetCart(int clientId);
        public CartData? GetCart(int clientId, int cartId);
        public CartData? AddCart(CartData cart);
        public void RemoveCart(int cartId);
        public CartData? UpdateCart(int cartId, CartData cart);
    }
}
