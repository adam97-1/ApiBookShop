using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApiBookShop.Models
{
    public class CartBook
    {
        public int CartBookId { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int BookQuantity { get; set; }
    }
}
