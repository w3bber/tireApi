using Microsoft.EntityFrameworkCore;
using TireApi.EfCore;
using TireApi.Repositories.Interfaces;

namespace TireApi.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly EF_DataContext _context;

        public ClientRepository(EF_DataContext context) => _context = context;

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Clients
                .AsNoTracking()
                .Include(c => c.Cars) // Подключаем машины клиента
                .ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _context.Clients
                .AsNoTracking()
                .Include(c => c.Cars) // Подключаем машины клиента
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            client.Cars ??= new List<Car>();
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> UpdateClientAsync(Client updatedClient)
        {
            var client = await _context.Clients.FindAsync(updatedClient.Id);
            if (client == null) return null;
            _context.Entry(client).CurrentValues.SetValues(updatedClient);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            var deletedRows = await _context.Clients
                .Where(id => id.Id == clientId)
                .ExecuteDeleteAsync();
            return deletedRows > 0;
        }
    }
}
