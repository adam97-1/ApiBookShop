namespace WebApiBookShop.Models
{
    public class OrderBook
    {
        public int OrderBookId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int BookQuantity { get; set; }
    }
}
