//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace EmployeePayroll
//{
//    public class SalaryRepository : ISalaryRepository
//    {
//        private readonly PayrollContext _context;
//        private readonly IPayrollLogger _logger;

//        public SalaryRepository(PayrollContext context, IPayrollLogger logger)
//        {
//            _context = context;
//            _logger = logger;
//        }
//        public async Task<IEnumerable<Salary>> GetAllAsync()
//        {
//            try
//            {
//                return await _context.Salaries
//               .Include(s => s.Employee)
//               .ToListAsync();
//            }
//            catch (Exception ex)
//            {
//                _logger.Log($"Error retrieving salaries: {ex.Message}");
//                return new List<Salary>();

//            }
//        }

//        public async Task SaveAsync(Salary salary)
//        {
//            try
//            {
//                _context.Salaries.Add(salary);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException dbEx)
//            {
//                _logger.Log($"Database error while saving salary: {dbEx.Message}");
//                throw;
//            }
//            catch (Exception ex)
//            {
//                _logger.Log($"if any error while saving salary: {ex.Message}");
//            }
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly PayrollContext _context;
        private readonly ILogger<SalaryRepository> _logger;

        public SalaryRepository(PayrollContext context, ILogger<SalaryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Salary>> GetAllAsync()
        {
            try
            {
                return await _context.Salaries
                    .Include(s => s.Employee)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving salaries");
                return new List<Salary>();
            }
        }

        public async Task SaveAsync(Salary salary)
        {
            try
            {
                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Salary saved successfully for employee {Name}", salary.Employee?.Name);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while saving salary for employee {Name}", salary.Employee?.Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while saving salary for employee {Name}", salary.Employee?.Name);
            }
        }
    }
}
