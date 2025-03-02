using Microsoft.EntityFrameworkCore;
using TireApi.EfCore;

namespace TireApi.Repositories
{
    public class CarRepository
    {
        private readonly EF_DataContext _context;
        public CarRepository(EF_DataContext context) => _context = context;

        public async Task<List<Car>> GetCarsAsync()
        {
            return await _context.Cars
                .AsNoTracking()
                .Include(c => c.Client)
                .ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars
                .AsNoTracking()
                .Include(c => c.Client)
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car> CreateCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car?> UpdateCarAsync(Car udatedCar)
        {
            var car = await _context.Cars.FindAsync(udatedCar.Id);
            if (car == null) return null;
            _context.Entry(car).CurrentValues.SetValues(car);
            return car;
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            var deletedRows = await _context.Cars
                .Where(id => id.Id == carId)
                .ExecuteDeleteAsync();
            return deletedRows > 0;
        }
    }
}
