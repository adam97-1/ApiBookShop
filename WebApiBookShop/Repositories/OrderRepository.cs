using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookShopContext _context;

        public OrderRepository(BookShopContext context)
        {
            _context = context;
        }

        public OrderData? AddOrder(OrderData order)
        {
            if (order.Order == null)
                return null;

            var newCart = _context.Orders.Add(order.Order);
            _context.SaveChanges();

            foreach (int? bookId in order.Books.Select(x => x.BookId).ToList())
            {
                if (bookId == null || !_context.Books.Any(x => x.BookId == bookId))
                    continue;

                OrderBook orderBook = new();

                BookSimple? book = order.Books.FirstOrDefault(x => x.BookId == bookId);
                if (book == null)
                    continue;

                orderBook.OrderId = order.Order.OrderId;
                orderBook.BookId = book.BookId;
                orderBook.BookQuantity = book.Quantity;

                _context.OrderBooks.Add(orderBook);
            }
            _context.SaveChanges();
            return order;
        }

        public OrderData? GetOrder(int orderId, int clientId)
        {
            OrderData orderData = new();

            Order? order = _context.Orders.Find(orderId);
            if (order == null || order.ClientId != clientId)
                return null;

            orderData.Order = order;

            IEnumerable<int> ordersBookId = _context.OrderBooks.Where(x => x.OrderId == order.OrderId).Select(row => row.OrderBookId).ToList();
            if (ordersBookId == null)
                return null;

            foreach (var orderBookId in ordersBookId)
            {
                OrderBook? cartBook = _context.OrderBooks.Where(x => x.OrderId == order.OrderId && x.OrderBookId == orderBookId).FirstOrDefault();
                if (cartBook == null)
                    continue;


                BookSimple book = new()
                {
                    BookId = cartBook.BookId,
                    Quantity = cartBook.BookQuantity
                };
                orderData.Books.Add(book);
            }

            return orderData;
        }

        public IEnumerable<OrderData>? GetOrders(int clientId)
        {
            List<OrderData> OrdersData = new();


            IEnumerable<Order> orders = _context.Orders.Where(x => x.ClientId == clientId);
            if (orders == null)
                return null;

            foreach (Order o in orders)
            {
                OrderData orderData = new();

                IEnumerable<int> orderBookIds = _context.OrderBooks.Where(x => x.OrderId == o.OrderId).Select(row => row.OrderBookId).ToList();
                if (orderBookIds == null)
                    continue;

                foreach (var orderBookId in orderBookIds)
                {

                    orderData.Order = o;

                    OrderBook? orderBook = _context.OrderBooks.Where(x => x.OrderId == o.OrderId && x.OrderBookId == orderBookId).FirstOrDefault();
                    if (orderBook == null)
                        continue;


                    BookSimple book = new()
                    {
                        BookId = orderBook.BookId,
                        Quantity = orderBook.BookQuantity
                    };
                    orderData.Books.Add(book);
                }

                OrdersData.Add(orderData);

            }
            return OrdersData;
        }

        public void RemoveOrder(int OrderId)
        {
            var order = _context.Orders.Find(OrderId);
            if (order == null)
                return;

            var orderBooks = _context.OrderBooks.Where(x => x.OrderId == OrderId);
            foreach (var d in orderBooks)
            {
                _context.OrderBooks.Remove(d);
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public OrderData? UpdateOrder(int OrderId, OrderData order)
        {
            if (order.Order == null || order.Order.OrderId != OrderId)
                return null;

            _context.Orders.Update(order.Order);

            IEnumerable<OrderBook> oldData = _context.OrderBooks.Where(x => x.OrderId == OrderId);
            foreach (var oredr in oldData)
            {
                _context.OrderBooks.Remove(oredr);
            }

            foreach (int? bookId in order.Books.Select(x => x.BookId).ToList())
            {
                if (bookId == null || !_context.Books.Any(x => x.BookId == bookId))
                    continue;

                OrderBook orderBook = new();

                BookSimple? book = order.Books.FirstOrDefault(x => x.BookId == bookId);
                if (book == null)
                    continue;

                orderBook.OrderId = order.Order.OrderId;
                orderBook.BookId = book.BookId;
                orderBook.BookQuantity = book.Quantity;

                _context.OrderBooks.Add(orderBook);
            }
            _context.SaveChanges();
            return order;
        }
    }
}
