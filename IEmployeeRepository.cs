using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id);      // async fetch
        Task<List<Employee>> GetAllAsync();        // async fetch all
        Task AddAsync(Employee employee);          // async add
        Task UpdateAsync(Employee employee);       // async update
        Task DeleteAsync(int id);                  // async delete
    }
}
