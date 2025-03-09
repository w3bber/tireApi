using TireApi.EfCore;

namespace TireApi.Repositories.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<ServiceType> CreateServiceTypeAsync(ServiceType serviceType);
        Task<bool> DeleteServiceTypeAsync(int serviceId);
        Task<ServiceType?> GetServiceTypeByIdAsync(int id);
        Task<List<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType?> UpdateServiceTypeAsync(ServiceType updatedServiceType);
    }
}