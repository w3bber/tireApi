using TireApi.Common;
using TireApi.Models;

namespace TireApi.Services.Interfaces
{
    public interface IServiceTypeService
    {
        Task<ApiResponse> CreateServiceTypeAsync(ServiceTypeModel serviceTypeModel);
        Task<ApiResponse> DeleteServiceTypeAsync(int serviceTypeId);
        Task<ApiResponse> GetServiceTypeByIdAsync(int id);
        Task<ApiResponse> GetServiceTypesAsync();
        Task<ApiResponse> UpdateServiceTypeAsync(ServiceTypeModel serviceTypeModel);
    }
}