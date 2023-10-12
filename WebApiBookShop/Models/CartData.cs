using System.Collections;

namespace WebApiBookShop.Models
{
    public class CartData
    {
        public Cart Cart { get; set; } = new();
        public List<BookSimple> Books { get; set; } = new List<BookSimple>();
    }
}
