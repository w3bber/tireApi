using TireApi.EfCore;

namespace TireApi.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> CreateClientAsync(Client client);
        Task<bool> DeleteClientAsync(int clientId);
        Task<Client?> GetClientByIdAsync(int id);
        Task<List<Client>> GetClientsAsync();
        Task<Client?> UpdateClientAsync(Client updatedClient);
    }
}