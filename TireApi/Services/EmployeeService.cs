using AutoMapper;
using TireApi.Common;
using TireApi.EfCore;
using TireApi.Models;
using TireApi.Repositories.Interfaces;
using TireApi.Services.Interfaces;

namespace TireApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            var mappedEmployees= _mapper.Map<List<EmployeeModel>>(employees);
            return mappedEmployees.Any()
                ? ResponseHandler.GetAppResponse(ResponseType.Success, mappedEmployees)
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "No employees found");
        }

        public async Task<ApiResponse> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return employee != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<EmployeeModel>(employee))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Employee not found");
        }

        public async Task<ApiResponse> CreateEmployeeAsync(EmployeeModel employeeModel)
        {
            if (employeeModel == null)
                return ResponseHandler.GetAppResponse(ResponseType.Failure, "Invalid client data");

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(_mapper.Map<Employee>(employeeModel));
            return ResponseHandler.GetAppResponse(ResponseType.Created, _mapper.Map<Employee>(employeeModel));
        }

        public async Task<ApiResponse> UpdateEmployeeAsync(EmployeeModel updatedEmployeeModel)
        {
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(_mapper.Map<Employee>(updatedEmployeeModel));
            return updatedEmployee != null
                ? ResponseHandler.GetAppResponse(ResponseType.Success, _mapper.Map<EmployeeModel>(updatedEmployee))
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Client not found");
        }

        public async Task<ApiResponse> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id)
                ? ResponseHandler.GetAppResponse(ResponseType.Deleted, "Client deleted")
                : ResponseHandler.GetAppResponse(ResponseType.NotFound, "Client not found");
        }
    }
}
