using TireApi.Models;
using TireApi.Common;

namespace TireApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse> CreateEmployeeAsync(EmployeeModel employeeModel);
        Task<ApiResponse> DeleteEmployeeAsync(int employeeId);
        Task<ApiResponse> GetEmployeeByIdAsync(int id);
        Task<ApiResponse> GetEmployeesAsync();
        Task<ApiResponse> UpdateEmployeeAsync(EmployeeModel employeeModel);
    }
}