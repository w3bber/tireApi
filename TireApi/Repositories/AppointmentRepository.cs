using Microsoft.EntityFrameworkCore;
using TireApi.EfCore;
using TireApi.Repositories.Interfaces;

namespace TireApi.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly EF_DataContext _context;

        public AppointmentRepository(EF_DataContext context) => _context = context;

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _context.Appointments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Include(a => a.Car)
                .Include(a => a.AppointmentServiceTypes)
                    .ThenInclude(ast => ast.ServiceType) // Загружаем услуги
                .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Include(a => a.Car)
                .Include(a => a.AppointmentServiceTypes)
                    .ThenInclude(ast => ast.ServiceType)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment?> UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(appointment.Id);
            if (existingAppointment == null) return null;
            existingAppointment.Status = appointment.Status;
            _context.Entry(existingAppointment).Property(a => a.Status).IsModified = true;
            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            var deletedRows = await _context.Clients
                .Where(id => id.Id == appointmentId)
                .ExecuteDeleteAsync();
            return deletedRows > 0;
        }
    }
}
