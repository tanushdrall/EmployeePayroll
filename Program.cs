//using System;
//using System.Threading.Tasks;
//using EmployeePayroll;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Configuration;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        ILogger<Program>? logger = null;


//        try
//        {
//            var config = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();

//            var services = new ServiceCollection();

//            services.AddDbContext<PayrollContext>(options => 

//    options.UseSqlServer(
//        config.GetConnectionString("PayrollDb")));

//            services.AddLogging(builder =>
//            {
//                builder.AddConsole();
//                builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
//            });

//            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
//            services.AddTransient<IValidator<Employee>, EmployeeValidator>();
//            services.AddTransient<ISalaryCalculator, SalaryCalculator>();
//            services.AddTransient<EmployeeService>();
//            services.AddTransient<PayrollService>();
//            services.AddTransient<ISalaryRepository, SalaryRepository>();

//            var provider = services.BuildServiceProvider();
//            var employeeService = provider.GetRequiredService<EmployeeService>();
//            var payrollService = provider.GetRequiredService<PayrollService>();
//            var logger = provider.GetRequiredService <ILogger<Program>>();

//            bool exit = false;
//            while (!exit)
//            {
//                Console.WriteLine("\n=== Payroll Menu ===");
//                Console.WriteLine("1. Add Employee");
//                Console.WriteLine("2. Process Payroll");
//                Console.WriteLine("3. View Employees");
//                Console.WriteLine("4. Exit");
//                Console.Write("Choose an option: ");

//                var choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        await AddEmployee(employeeService, logger);
//                        break;

//                    case "2":
//                        await payrollService.ProcessPayrollAsync();
//                        logger.LogInformation("Payroll processed successfully.");
//                        break;

//                    case "3":
//                        await ViewEmployees(employeeService, logger);
//                        break;

//                    case "4":
//                        exit = true;
//                        break;

//                    default:
//                        logger.LogWarning("Invalid option.");
//                        break;
//                }
//            }

//            logger.LogInformation("Application shutting down safely.");
//        }
//        catch (DbUpdateException dbEx)
//        {
//            logger.LogError(dbEx, "Database error occurred.");
//            throw;

//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Fatal error: {ex.Message}");
//            throw;

//        }
//    }

//    private static async Task ViewEmployees(EmployeeService employeeService, ILogger<Program> logger)
//    {
//        var employees = await employeeService.GetEmployeesAsync();

//        foreach (var emp in employees)
//        {
//            logger.LogInformation($"Id: {emp.Id} | Name: {emp.Name} | Dept: {emp.DepartmentId} | Salary: {emp.BaseSalary}");
//        }
//    }

//    private static async Task AddEmployee(EmployeeService employeeService, ILogger<Program> logger)
//    {
//        Console.WriteLine("Departments : 1-HR, 2-Finance, 3-IT");
//        Console.Write("Name: ");
//        var name = Console.ReadLine();

//        Console.Write("Department Id: ");
//        if (!int.TryParse(Console.ReadLine(), out int deptId))
//        {
//            logger.LogError("Invalid department Id.");
//            return;
//        }

//        Console.Write("Base Salary: ");
//        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
//        {
//            logger.LogError("Invalid salary.");
//            return;
//        }

//        var employee = new Employee
//        {
//            Name = name,
//            DepartmentId = deptId,
//            BaseSalary = salary
//        };

//        await employeeService.AddEmployeeAsync(employee);
//        logger.LogInformation($"Employee {name} added successfully.");
//    }
//}

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
        foreach (var emp in employees)
        {
            logger.LogInformation(
                "Id: {Id} | Name: {Name} | Dept: {DeptId} | Salary: {Salary}",
                emp.Id, emp.Name, emp.DepartmentId, emp.BaseSalary);
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
        if (!int.TryParse(Console.ReadLine(), out int deptId))
        {
            logger.LogWarning("Invalid department Id entered.");
            return;
        }

        Console.Write("Base Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            logger.LogWarning("Invalid salary entered.");
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