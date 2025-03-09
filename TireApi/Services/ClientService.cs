using AutoMapper;
using TireApi.Common;
using TireApi.EfCore;
using TireApi.Models;
using TireApi.Repositories.Interfaces;
using TireApi.Services.Interfaces;

namespace TireApi.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetClientsAsync()
        {
            var clients = await _clientRepository.GetClientsAsync();
            var mappedClients = _mapper.Map<List<ClientModel>>(clients);
            return mappedClients.Any()
                ? ResponseHandler.GetAppResponse(ResponseType.Success, mappedClients)
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "No clients found");
        }

        public async Task<ApiResponse> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            return client != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<ClientModel>(client))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Client not found");
        }

        public async Task<ApiResponse> CreateClientAsync(ClientModel clientModel)
        {
            if (clientModel == null)
                return ResponseHandler.GetAppResponse(ResponseType.Failure, "Invalid client data");

            var createdClient = await _clientRepository.CreateClientAsync(_mapper.Map<Client>(clientModel));
            return ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<ClientModel>(createdClient));
        }

        public async Task<ApiResponse> UpdateClientAsync(ClientModel updatedClientModel)
        {
            var updatedClient = await _clientRepository.UpdateClientAsync(_mapper.Map<Client>(updatedClientModel));
            return updatedClient != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<ClientModel>(updatedClient))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Client not found");
        }

        public async Task<ApiResponse> DeleteClientAsync(int id)
        {
            return await _clientRepository.DeleteClientAsync(id)
                ? ResponseHandler.GetAppResponse(ResponseType.Success, "Client deleted")
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Client not found");
        }
    }
}
