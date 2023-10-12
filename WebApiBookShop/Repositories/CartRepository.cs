using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BookShopContext _context;

        public CartRepository(BookShopContext context)
        {
            _context = context;
        }

 

        public CartData? AddCart(CartData cart)
        {
            if (cart.Cart == null)
                return null;

            var newCart = _context.Carts.Add(cart.Cart);
            _context.SaveChanges();

            foreach (int? bookId in cart.Books.Select(x => x.BookId).ToList())
            {
                if (bookId == null || !_context.Books.Any(x => x.BookId == bookId))
                    continue;

                CartBook cartBook = new();

                BookSimple? book = cart.Books.FirstOrDefault(x => x.BookId == bookId);
                if (book == null)
                    continue;

                cartBook.CartId = cart.Cart.CartId;
                cartBook.BookId = book.BookId;
                cartBook.BookQuantity = book.Quantity;

                _context.CartBooks.Add(cartBook);
            }
            _context.SaveChanges();
            return cart;
        }

        public IEnumerable<CartData>? GetCart(int clientId)
        {
            List<CartData> cartsData = new();
           
            
            IEnumerable<Cart> carts = _context.Carts.Where(x => x.ClientId == clientId);
            if (carts == null)
                return null;

            foreach (Cart c in carts)
            { 
                CartData cartData = new();

                IEnumerable<int> cartBookIds = _context.CartBooks.Where(x => x.CartId == c.CartId).Select(row => row.CartBookId).ToList();
                if (cartBookIds == null)
                    continue;

                foreach (var cartBookId in cartBookIds)
                {
                   
                    cartData.Cart = c;

                    CartBook? cartBook = _context.CartBooks.Where(x => x.CartId == c.CartId && x.CartBookId == cartBookId).FirstOrDefault();
                    if (cartBook == null)
                        continue;


                    BookSimple book = new()
                    {
                        BookId = cartBook.BookId,
                        Quantity = cartBook.BookQuantity
                    };
                    cartData.Books.Add(book);
                }

                cartsData.Add(cartData);
               
            }
            return cartsData;

        }

        public CartData? GetCart(int clientId, int cartId)
        {
            CartData cartData = new();

            Cart? cart = _context.Carts.Find(cartId);
            if (cart == null || cart.ClientId != clientId)
                return null;
 
            cartData.Cart = cart;

            IEnumerable<int> cartBookIds = _context.CartBooks.Where(x => x.CartId == cart.CartId).Select(row => row.CartBookId).ToList();
            if (cartBookIds == null)
                return null;

            foreach (var cartBookId in cartBookIds)
            {
                CartBook? cartBook = _context.CartBooks.Where(x => x.CartId == cart.CartId && x.CartBookId == cartBookId).FirstOrDefault();
                if (cartBook == null)
                    continue;


                BookSimple book = new()
                {
                    BookId = cartBook.BookId,
                    Quantity = cartBook.BookQuantity
                };
                cartData.Books.Add(book);
            }

            return cartData;
        }

        public void RemoveCart(int CartId)
        {
            var cart = _context.Carts.Find(CartId);
            if (cart == null)
                return;

            var cartBooks = _context.CartBooks.Where(x => x.CartId == CartId);
            foreach(var d in cartBooks)
            {
                _context.CartBooks.Remove(d);
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public CartData? UpdateCart(int CartId, CartData cart)
        {
            if (cart.Cart == null || cart.Cart.CartId != CartId)
                return null;

            _context.Carts.Update(cart.Cart);

            IEnumerable<CartBook>  oldData = _context.CartBooks.Where(x => x.CartId == CartId);
            foreach (CartBook book in oldData)
            {
                _context.CartBooks.Remove(book);
            }

            foreach (int? bookId in cart.Books.Select(x => x.BookId).ToList())
            {
                if (bookId == null || !_context.Books.Any(x => x.BookId == bookId))
                    continue;

                CartBook cartBook = new();

                BookSimple? book = cart.Books.FirstOrDefault(x => x.BookId == bookId);
                if (book == null)
                    continue;

                cartBook.CartId = cart.Cart.CartId;
                cartBook.BookId= book.BookId;
                cartBook.BookQuantity = book.Quantity;

                _context.CartBooks.Add(cartBook);
            }
            _context.SaveChanges();
            return cart;
        }
    }
}
