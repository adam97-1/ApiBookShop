namespace WebApiBookShop.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = "User";
    }
}
