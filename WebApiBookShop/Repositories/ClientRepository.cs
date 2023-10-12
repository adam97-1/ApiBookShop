using Microsoft.IdentityModel.Tokens;
using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BookShopContext _context;

        public ClientRepository(BookShopContext context)
        {
            _context = context;
        }

        public Client? AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public Client? GetClient(int id)
        {
            return _context.Clients.Find(id);
        }

        public IEnumerable<Client>? GetClients()
        {
            return _context.Clients.ToArray();
        }

        public void RemoveClient(int id)
        {
            var c = _context.Clients.Find(id);
            if (c != null)
            {
                _context.Clients.Remove(c);
                _context.SaveChanges();
            }
        }

        public Client? UpdateClient(int id, Client client)
        {
            if (id != client.ClientId)
                return null;
            _context.Clients.Update(client);
            _context.SaveChanges();
            return client;
        }
    }
}
