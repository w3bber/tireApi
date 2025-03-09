using TireApi.Common;
using TireApi.Models;

namespace TireApi.Services.Interfaces
{
    public interface ICarService
    {
        Task<ApiResponse> CreateCarAsync(CarModel model);
        Task<ApiResponse> DeleteCarAsync(int id);
        Task<ApiResponse> GetCarByIdAsync(int id);
        Task<ApiResponse> GetCarsAsync();
        Task<ApiResponse> UpdateCarAsync(CarModel model);
    }
}