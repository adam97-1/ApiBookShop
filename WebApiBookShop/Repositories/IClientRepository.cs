using WebApiBookShop.Models;

namespace WebApiBookShop.Repositories
{
    public interface IClientRepository
    {
        public Client ?AddClient(Client client);
        public Client ?UpdateClient(int id, Client client);
        public void RemoveClient(int id);
        public Client ?GetClient(int id);
        public IEnumerable<Client> GetClients();
    }
}
