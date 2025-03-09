using TireApi.EfCore;

namespace TireApi.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int appointmentId);
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<List<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> UpdateAppointmentAsync(Appointment appointment);
    }
}