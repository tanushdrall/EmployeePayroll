using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IValidator<Employee> _validator;
        private readonly IPayrollLogger _logger;

        public EmployeeService(IEmployeeRepository repository,
                               IValidator<Employee> validator,
                               IPayrollLogger logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Employee?> AddEmployeeAsync(Employee employee)
        {
            try
            {
                if (!_validator.Validate(employee))
                {
                    _logger.Log("Employee validation failed.");
                    return null;
                }

                await _repository.AddAsync(employee);
                _logger.Log($"Employee {employee.Name} added successfully.");
                return employee;
            }
            catch (Exception ex)
            {
                _logger.Log($"Error adding employee: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.Log($"Error retrieving employees: {ex.Message}");
                return new List<Employee>();
            }
        }
    }
}
