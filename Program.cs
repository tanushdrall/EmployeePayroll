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
            services.AddSingleton<IPayrollLogger, ConsolePayrollILogger>();
            services.AddTransient<ISalaryRepository, SalaryRepository>();

            var provider = services.BuildServiceProvider();
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
                        await AddEmployee(employeeService);
                        break;

                    case "2":
                        await payrollService.ProcessPayrollAsync();
                        break;

                    case "3":
                        await ViewEmployees(employeeService);
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }

            Console.WriteLine("Application shutting down safely.");
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine($"Database error: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
        }
    }

    private static async Task ViewEmployees(EmployeeService employeeService)
    {
        var employees = await employeeService.GetEmployeesAsync();

        foreach (var emp in employees)
        {
            Console.WriteLine($"Id: {emp.Id} | Name: {emp.Name} | Dept: {emp.DepartmentId} | Salary: {emp.BaseSalary}");
        }
    }

    private static async Task AddEmployee(EmployeeService employeeService)
    {
        Console.WriteLine("Departments : 1-HR, 2-Finance, 3-IT");
        Console.Write("Name: ");
        var name = Console.ReadLine();

        Console.Write("Department Id: ");
        if (!int.TryParse(Console.ReadLine(), out int deptId))
        {
            Console.WriteLine("Invalid department Id.");
            return;
        }

        Console.Write("Base Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        var employee = new Employee
        {
            Name = name,
            DepartmentId = deptId,
            BaseSalary = salary
        };

        await employeeService.AddEmployeeAsync(employee);
        Console.WriteLine($"Employee {name} added successfully.");
    }
}
