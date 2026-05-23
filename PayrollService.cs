
//using EmployeePayroll;


//public class PayrollService
//{
//    private readonly IEmployeeRepository _employeeRepository;
//    private readonly ISalaryRepository _salaryRepository;
//    private readonly ISalaryCalculator _calculator;
//    private readonly IValidator<Employee> _validator;
//    private readonly IPayrollLogger _logger;

//    public PayrollService(
//        IEmployeeRepository employeeRepository,
//        ISalaryRepository salaryRepository,
//        ISalaryCalculator calculator,
//        IValidator<Employee> validator,
//        IPayrollLogger logger)
//    {
//        _employeeRepository = employeeRepository;
//        _salaryRepository = salaryRepository;
//        _calculator = calculator;
//        _validator = validator;
//        _logger = logger;
//    }

//    public async Task ProcessPayrollAsync()
//    {
//        var employees = await _employeeRepository.GetAllAsync();

//        foreach (var emp in employees)
//        {
//            try
//            {

//                if (!_validator.Validate(emp))
//                {
//                    _logger.Log($"Invalid employee {emp.Name}. Skipped.");
//                    continue;
//                }

//                var salary = _calculator.Calculate(emp);
//                try
//                {
//                    await _salaryRepository.SaveAsync(salary);
//                    _logger.Log($"Payroll processed for {emp.Name}, Net: {salary.NetSalary:F2}");
//                }
//                catch (Exception saveEx)
//                {
//                    _logger.Log($"Error processing payroll for {emp.Name}: {saveEx.Message}");

//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.Log($"Unexpected error processing employee {emp.Name}: {ex.Message}");

//            }

//        }
//    }
//}

using Microsoft.Extensions.Logging;

namespace EmployeePayroll
{
    public class PayrollService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly ISalaryCalculator _calculator;
        private readonly IValidator<Employee> _validator;
        private readonly ILogger<PayrollService> _logger;

        public PayrollService(
            IEmployeeRepository employeeRepository,
            ISalaryRepository salaryRepository,
            ISalaryCalculator calculator,
            IValidator<Employee> validator,
            ILogger<PayrollService> logger)
        {
            _employeeRepository = employeeRepository;
            _salaryRepository = salaryRepository;
            _calculator = calculator;
            _validator = validator;
            _logger = logger;
        }

        public async Task ProcessPayrollAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            foreach (var emp in employees)
            {
                try
                {
                    if (!_validator.Validate(emp))
                    {
                        _logger.LogWarning("Invalid employee {Name}. Skipped.", emp.Name);
                        continue;
                    }

                    var salary = _calculator.Calculate(emp);

                    try
                    {
                        await _salaryRepository.SaveAsync(salary);
                        _logger.LogInformation("Payroll processed for {Name}, Net: {NetSalary:F2}", emp.Name, salary.NetSalary);
                    }
                    catch (Exception saveEx)
                    {
                        _logger.LogError(saveEx, "Error processing payroll for {Name}", emp.Name);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error processing employee {Name}", emp.Name);
                }
            }
        }
    }
}

