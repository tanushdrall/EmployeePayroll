using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EmployeePayroll
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IValidator<Employee> _validator;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository repository,
                               IValidator<Employee> validator,
                               ILogger<EmployeeService> logger)
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
                    _logger.LogWarning("Employee validation failed for {Name}", employee.Name);
                    return null;
                }

                await _repository.AddAsync(employee);
                _logger.LogInformation("Employee {Name} added successfully", employee.Name);
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding employee {Name}", employee.Name);
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
                _logger.LogError(ex, "Error retrieving employees");
                return new List<Employee>();
            }
        }
    }
}
