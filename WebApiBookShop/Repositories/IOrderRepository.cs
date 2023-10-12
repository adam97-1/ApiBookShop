using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public interface IOrderRepository
    {
        public OrderData? AddOrder(OrderData order);
        public OrderData? GetOrder(int orderId, int clientId);
        public IEnumerable<OrderData>? GetOrders(int clientId);
        public void RemoveOrder(int OrderId);
        public OrderData? UpdateOrder(int OrderId, OrderData order);
    }
}
