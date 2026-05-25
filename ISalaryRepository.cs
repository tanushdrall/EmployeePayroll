using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public interface ISalaryRepository   
    {
        
        Task<IEnumerable<Salary>> GetAllAsync();
        Task SaveAsync(Salary salary);
    }
}