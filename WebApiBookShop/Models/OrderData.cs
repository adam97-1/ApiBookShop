namespace WebApiBookShop.Models
{
    public class OrderData
    {
        public Order? Order { get; set; }
        public List<BookSimple> Books { get; set; } = new List<BookSimple>();
    }
}
