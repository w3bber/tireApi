using TireApi.Models;
using TireApi.Common;
namespace TireApi.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<ApiResponse> CreateAppointmentAsync(AppointmentModel appointmentModel);
        Task<ApiResponse> DeleteAppointmentAsync(int appointmentId);
        Task<ApiResponse> GetAppointmentByIdAsync(int id);
        Task<ApiResponse> GetAppointmentsAsync();
        Task<ApiResponse> UpdateAppointmentAsync(AppointmentModel appointmentModel);
    }
}