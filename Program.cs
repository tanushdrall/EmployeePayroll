using System;
using System.Threading.Tasks;
using EmployeePayroll;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        ILogger<Program>? logger = null;

        try
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<PayrollContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("PayrollDb")));

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
            });

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IValidator<Employee>, EmployeeValidator>();
            services.AddTransient<ISalaryCalculator, SalaryCalculator>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<PayrollService>();
            services.AddTransient<ISalaryRepository, SalaryRepository>();

            var provider = services.BuildServiceProvider();

            logger = provider.GetRequiredService<ILogger<Program>>();
            var employeeService = provider.GetRequiredService<EmployeeService>();
            var payrollService = provider.GetRequiredService<PayrollService>();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== Payroll Menu ===");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Process Payroll");
                Console.WriteLine("3. View Employees");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddEmployee(employeeService, logger);
                        break;
                    case "2":
                        await payrollService.ProcessPayrollAsync();
                        logger.LogInformation("Payroll processed successfully.");
                        break;
                    case "3":
                        await ViewEmployees(employeeService, logger);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        logger.LogWarning("Invalid option selected.");
                        break;
                }
            }

            logger.LogInformation("Application shutting down safely.");
        }
        catch (DbUpdateException dbEx)
        {
            if (logger != null)
                logger.LogError(dbEx, "Database error occurred.");

            else
                Console.WriteLine($"Database error: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            if (logger != null)
                logger.LogError(ex, "Fatal error occurred.");

            else
                Console.WriteLine($"Fatal error: {ex.Message}");
        }
    }

    private static async Task ViewEmployees(
    EmployeeService employeeService,
    ILogger<Program> logger)
    {
        var employees = await employeeService.GetEmployeesAsync();

        Console.WriteLine("\n=== Employee List ===");
        Console.WriteLine("ID   | Name    | Dept | Salary");
        Console.WriteLine("-------------------------------");

        foreach (var emp in employees)
        {
            var deptName = emp.Department?.Name ?? "Unknown";
            Console.WriteLine($"{emp.Id,-4} | {emp.Name,-7} | {deptName,-7} | {emp.BaseSalary,8:F2}");

        }
    }

    private static async Task AddEmployee(
        EmployeeService employeeService,
        ILogger<Program> logger)
    {
        Console.WriteLine("Departments: 1-HR, 2-Finance, 3-IT");
        Console.Write("Name: ");
        var name = Console.ReadLine();
        
        Console.Write("Department Id: ");

        if (!int.TryParse(Console.ReadLine(), out int deptId) || deptId < 1 || deptId > 3)
        {
            logger.LogWarning("Invalid department Id. Must be 1, 2, or 3.");
            return;
        }

        Console.Write("Base Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary <= 0 || salary > 500000)
        {
            logger.LogWarning("Invalid salary entered. Must be > 0 and <= 1,000,000.");
            return;
        }
        var existing = await employeeService.GetEmployeesAsync();
        if (existing.Any(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            logger.LogWarning("Employee with name {Name} already exists.", name);
            return;
        }

        var employee = new Employee
        {
            Name = name,
            DepartmentId = deptId,
            BaseSalary = salary
        };

        var result = await employeeService.AddEmployeeAsync(employee);
        if (result != null)
            logger.LogInformation("Employee {Name} added successfully.", name);
        else
            logger.LogWarning("Employee {Name} was not added.", name);
    }
    
}