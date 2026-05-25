using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeePayroll
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PayrollContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(PayrollContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Employee employee)
        {
            try
            {
                employee.Id = 0;
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Employee {Name} added successfully", employee.Name);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while adding employee {Name}", employee.Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in AddAsync for employee {Name}", employee.Name);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Employee {Id} deleted successfully", id);
                }
                else
                {
                    _logger.LogWarning("Attempted to delete non-existent employee {Id}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee {Id}", id);
                throw;
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            try
            {
                return await _context.Employees
                    .Include(e => e.Department)  
                    .Include(e => e.Salaries)     
                    .AsNoTracking()               
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving employees");
                return new List<Employee>();
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.Salaries)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting employee by Id {Id}", id);
                return null;
            }
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                var existing = await _context.Employees.FindAsync(employee.Id);
                if (existing != null)
                {
                    existing.Name = employee.Name;
                    existing.DepartmentId = employee.DepartmentId;
                    existing.BaseSalary = employee.BaseSalary;

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Employee {Id} updated successfully", employee.Id);
                }
                else
                {
                    _logger.LogWarning("Attempted to update non-existent employee {Id}", employee.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee {Id}", employee.Id);
                throw;
            }
        }
    }
}
