using Microsoft.EntityFrameworkCore;
using TireApi.EfCore;
using TireApi.Repositories.Interfaces;

namespace TireApi.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly EF_DataContext _context;

        public ServiceTypeRepository(EF_DataContext context) => _context = context;

        public async Task<List<ServiceType>> GetServiceTypesAsync()
        {
            return await _context.ServiceTypes
                .AsNoTracking()
                .Include(c => c.AppointmentServiceTypes)
                .ToListAsync();
        }

        public async Task<ServiceType?> GetServiceTypeByIdAsync(int id)
        {
            return await _context.ServiceTypes
                .AsNoTracking()
                .Include(c => c.AppointmentServiceTypes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ServiceType> CreateServiceTypeAsync(ServiceType serviceType)
        {
            _context.ServiceTypes.Add(serviceType);
            await _context.SaveChangesAsync();
            return serviceType;
        }

        public async Task<ServiceType?> UpdateServiceTypeAsync(ServiceType updatedServiceType)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(updatedServiceType.Id);
            if (serviceType == null) return null;
            _context.Entry(serviceType).CurrentValues.SetValues(updatedServiceType);
            return serviceType;
        }

        public async Task<bool> DeleteServiceTypeAsync(int serviceId)
        {
            var deletedRows = await _context.ServiceTypes
                .Where(id => id.Id == serviceId)
                .ExecuteDeleteAsync();
            return deletedRows > 0;
        }

    }
}
