using TireApi.Models;
using TireApi.Common;

namespace TireApi.Services.Interfaces
{
    public interface IClientService
    {
        Task<ApiResponse> CreateClientAsync(ClientModel clientModel);
        Task<ApiResponse> DeleteClientAsync(int id);
        Task<ApiResponse> GetClientByIdAsync(int id);
        Task<ApiResponse> GetClientsAsync();
        Task<ApiResponse> UpdateClientAsync(ClientModel updatedClientModel);
    }
}