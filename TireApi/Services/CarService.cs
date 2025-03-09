using AutoMapper;
using TireApi.Common;
using TireApi.EfCore;
using TireApi.Models;
using TireApi.Repositories.Interfaces;
using TireApi.Services.Interfaces;

namespace TireApi.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetCarsAsync()
        {
            try
            {
                var cars = await _carRepository.GetCarsAsync();
                var carsModel = _mapper.Map<List<CarModel>>(cars);

                return carsModel.Any()
                    ? ResponseHandler.GetAppResponse(ResponseType.Success, carsModel)
                    : ResponseHandler.GetAppResponse(ResponseType.NotFound, "No cars found");
            }
            catch (Exception ex)
            {
                return ResponseHandler.GetExceptionResponse(ex);
            }
        }

        public async Task<ApiResponse> GetCarByIdAsync(int id)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(id);
                if (car == null) return ResponseHandler.GetAppResponse(ResponseType.NotFound, "Car not found");

                var carModel = _mapper.Map<CarModel>(car);
                return ResponseHandler.GetAppResponse(ResponseType.Success, carModel);
            }
            catch (Exception ex)
            {
                return ResponseHandler.GetExceptionResponse(ex);
            }
        }

        public async Task<ApiResponse> CreateCarAsync(CarModel model)
        {
            try
            {
                if (model == null)
                    return ResponseHandler.GetAppResponse(ResponseType.Failure, "Invalid car data");

                var entity = _mapper.Map<Car>(model);
                var createdEntity = await _carRepository.CreateCarAsync(entity);

                return ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<CarModel>(createdEntity));
            }
            catch (Exception ex)
            {
                return ResponseHandler.GetExceptionResponse(ex);
            }
        }

        public async Task<ApiResponse> UpdateCarAsync(CarModel model)
        {
            try
            {
                var entity = _mapper.Map<Car>(model);
                var updatedEntity = await _carRepository.UpdateCarAsync(entity);

                if (updatedEntity == null)
                    return ResponseHandler.GetAppResponse(ResponseType.NotFound, "Car not found");

                return ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<CarModel>(updatedEntity));
            }
            catch (Exception ex)
            {
                return ResponseHandler.GetExceptionResponse(ex);
            }
        }

        public async Task<ApiResponse> DeleteCarAsync(int id)
        {
            try
            {
                bool isDeleted = await _carRepository.DeleteCarAsync(id);
                return isDeleted
                    ? ResponseHandler.GetAppResponse(ResponseType.Success, "Car deleted")
                    : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Car not found");
            }
            catch (Exception ex)
            {
                return ResponseHandler.GetExceptionResponse(ex);
            }
        }
    }
}
