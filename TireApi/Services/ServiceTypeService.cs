using AutoMapper;
using TireApi.Common;
using TireApi.EfCore;
using TireApi.Models;
using TireApi.Repositories.Interfaces;
using TireApi.Services.Interfaces;

namespace TireApi.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetServiceTypesAsync()
        {

            var serviceTypes = await _serviceTypeRepository.GetServiceTypesAsync();
            var mappedServiceTypes = _mapper.Map<List<ServiceTypeModel>>(serviceTypes);
            return mappedServiceTypes.Any()
                ? ResponseHandler.GetAppResponse(ResponseType.Success, mappedServiceTypes)
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "No service types found");
        }

        public async Task<ApiResponse> GetServiceTypeByIdAsync(int id)
        {
            var serviceType = await _serviceTypeRepository.GetServiceTypeByIdAsync(id);
            return serviceType != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<ServiceTypeModel>(serviceType))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Service type not found");
        }

        public async Task<ApiResponse> CreateServiceTypeAsync(ServiceTypeModel serviceTypeModel)
        {
            if (serviceTypeModel == null)
                return ResponseHandler.GetAppResponse(ResponseType.BadRequest, "Invalid data");
            var createdServiceType = await _serviceTypeRepository.CreateServiceTypeAsync(_mapper.Map<ServiceType>(serviceTypeModel));
            return ResponseHandler.GetAppResponse(ResponseType.Created, _mapper.Map<ServiceTypeModel>(createdServiceType));
        }

        public async Task<ApiResponse> UpdateServiceTypeAsync(ServiceTypeModel serviceTypeModel)
        {
            if (serviceTypeModel == null)
                return ResponseHandler.GetAppResponse(ResponseType.Failure, "Invalid data");
            var updatedServiceType = _mapper.Map<ServiceType>(serviceTypeModel);
            var result = await _serviceTypeRepository.UpdateServiceTypeAsync(updatedServiceType);
            return ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<ServiceTypeModel>(result));
        }

        public async Task<ApiResponse> DeleteServiceTypeAsync(int serviceTypeId)
        {
            return await _serviceTypeRepository.DeleteServiceTypeAsync(serviceTypeId)
                ? ResponseHandler.GetAppResponse(ResponseType.Deleted, "service type deleted")
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "servicy type not found");
        }
    }
}
