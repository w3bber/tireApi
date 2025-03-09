using TireApi.EfCore;

namespace TireApi.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<Car> CreateCarAsync(Car car);
        Task<bool> DeleteCarAsync(int carId);
        Task<Car?> GetCarByIdAsync(int id);
        Task<List<Car>> GetCarsAsync();
        Task<Car?> UpdateCarAsync(Car udatedCar);
    }
}