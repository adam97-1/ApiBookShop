using System.ComponentModel.DataAnnotations;

namespace WebApiBookShop.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int ClientId { get; set; }
        public string? Description { get; set; }
    }
}
