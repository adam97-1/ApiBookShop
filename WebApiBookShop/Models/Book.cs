﻿namespace WebApiBookShop.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
