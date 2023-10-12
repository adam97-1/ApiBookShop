namespace WebApiBookShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string? Status { get; set; }
    }
}
