using Microsoft.EntityFrameworkCore;
using TireApi.EfCore;
using TireApi.Models;
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
            var existingAppointment = await _context.Appointments
                .Include(a => a.AppointmentServiceTypes)
                .FirstOrDefaultAsync(a => a.Id == appointment.Id);
            if (existingAppointment == null) return null;
            _context.Entry(existingAppointment).CurrentValues.SetValues(appointment);
            var existingServiceTypeIds = existingAppointment.AppointmentServiceTypes?
                .Select(ast => ast.ServiceTypeId)
                .ToList() ?? new List<int>();
            var newServiceTypeIds = appointment.AppointmentServiceTypes?
                .Select(ast => ast.ServiceTypeId)
                .ToList() ?? new List<int>();
            var serviceTypeIdsToRemove = existingServiceTypeIds
                .Except(newServiceTypeIds)
                .ToList();
            var serviceTypeIdsToAdd = newServiceTypeIds
                .Except(existingServiceTypeIds)
                .ToList();
            if (serviceTypeIdsToRemove.Any())
            {
                var toRemove = existingAppointment.AppointmentServiceTypes?
                    .Where(ast => serviceTypeIdsToRemove.Contains(ast.ServiceTypeId))
                    .ToList();

                if (toRemove != null)
                {
                    foreach (var item in toRemove)
                    {
                        existingAppointment.AppointmentServiceTypes?.Remove(item);
                    }
                }
            }
            foreach (var serviceTypeId in serviceTypeIdsToAdd)
            {
                existingAppointment.AppointmentServiceTypes?.Add(new AppointmentServiceType
                {
                    AppointmentId = existingAppointment.Id,
                    ServiceTypeId = serviceTypeId
                });
            }
            await _context.SaveChangesAsync();
            return existingAppointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            var deletedRows = await _context.Appointments
                .Where(id => id.Id == appointmentId)
                .ExecuteDeleteAsync();
            return deletedRows > 0;
        }
    }
}
