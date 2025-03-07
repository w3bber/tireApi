﻿using Microsoft.EntityFrameworkCore;
using TireApi.EfCore;

namespace TireApi.Repositories
{
    public class EmployeeRepository
    {
        private readonly EF_DataContext _context;
        public EmployeeRepository(EF_DataContext context) => _context = context;

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(c => c.Appointments)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include (c => c.Appointments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee updatedEmployee)
        {
            var employee = await _context.Employees.FindAsync(updatedEmployee.Id);
            if (employee == null) return null;
            _context.Entry(employee).CurrentValues.SetValues(employee);
            return employee;
        }

        public async Task<bool> DeleteEmlployeeAsync(int emplyeeId)
        {
            var deletedRows = await _context.Employees
                .Where(id => id.Id == emplyeeId)
                .ExecuteDeleteAsync();
            return deletedRows > 0;
        }
    }
}
