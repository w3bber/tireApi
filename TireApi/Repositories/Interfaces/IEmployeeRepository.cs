using TireApi.EfCore;

namespace TireApi.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int emplyeeId);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> UpdateEmployeeAsync(Employee updatedEmployee);
    }
}