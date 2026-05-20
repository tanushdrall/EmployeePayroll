# Employee Payroll System

A C# console application built with clean architecture principles.
Covers SOLID, async/await, dependency injection, EF Core, logging, validation, and unit tests.
All code written manually to build genuine understanding.

---

## What this app does

- Add employees via console menu
- View all employees from database
- Process payroll for all employees
- Save salary records to SQL Server
- Validate all input before processing
- Log all operations consistently

---

## Core concepts

### SOLID
```
S  →  Employee, EmployeeService, PayrollService, SalaryCalculator — each one job
O  →  New calculator = new class, existing untouched
L  →  Implementations swap via interfaces without breaking callers
I  →  IEmployeeRepository, ISalaryCalculator, IValidator — focused interfaces
D  →  All services depend on interfaces, injected via constructor
```

### Dependency Injection
```
ServiceCollection    →  registers all services
BuildServiceProvider →  creates container
Transient            →  new instance per request (services, repositories)
Singleton            →  one instance (logger)
GetRequiredService   →  throws if not registered
```

### Async/Await
```
All repository methods use Async suffix
SaveChangesAsync, ToListAsync, FindAsync throughout
async Task Main
No sync over async
```

### EF Core
```
PayrollContext — DbSet for Employee, Department, Salary
Code-first migrations
HasData seeding for departments and employees
HasColumnType decimal(18,2) for salary precision
IDENTITY_INSERT awareness — Id set to 0 before insert
```

### Logging
```
ILogger<T>       →  Microsoft built-in for repositories
IPayrollLogger   →  custom interface for services
LogInformation   →  success
LogWarning       →  non-critical issues
LogError         →  failures with exception
EF Core noise filtered in AddLogging
```

### Validation
```
IValidator<T>        →  generic interface
EmployeeValidator    →  name not empty, salary > 0, department > 0
Validated before any DB operation
```

### Error Handling
```
try/catch in all repository and service methods
DbUpdateException caught specifically for DB errors
throw after log — caller aware of failure
Inner try/catch in payroll loop — one failure does not stop others
```

### Configuration
```
appsettings.json  →  connection string
Not hardcoded in code
ConfigurationBuilder reads at startup
Excluded from source control via .gitignore
```

### Unit Tests
```
EmployeeValidatorTests  →  7 cases
SalaryCalculatorTests   →  calculation verified
All passing
```

---

## Project structure

```
EmployeePayroll/
│
├── Employee.cs
├── Department.cs
├── Salary.cs
├── IEmployeeRepository.cs
├── ISalaryRepository.cs
├── ISalaryCalculator.cs
├── IValidator.cs
├── IPayrollLogger.cs
├── EmployeeRepository.cs
├── SalaryRepository.cs
├── EmployeeService.cs
├── PayrollService.cs
├── SalaryCalculator.cs
├── EmployeeValidator.cs
├── ConsolePayrollILogger.cs
├── PayrollContext.cs
├── PayrollContextFactory.cs
├── appsettings.json
└── Program.cs
```

---

## How to run

1. Add your connection string to appsettings.json
2. Run migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
3. Run:
```bash
dotnet run
```

---

## Sample output

```
=== Payroll Menu ===
1. Add Employee
2. Process Payroll
3. View Employees
4. Exit

[PayrollLog] Payroll processed for Tanush, Net: 45000.00
[PayrollLog] Payroll processed for Aditi, Net: 40500.00
[PayrollLog] Payroll processed for Ravi, Net: 54000.00
```

---

## NuGet packages

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Logging.Console
xunit
xunit.runner.visualstudio
```

---

## Known limitations

```
No authentication or authorization
No role based access
Console only — no web layer
Department not validated against DB on add
No update or delete employee via menu
No CancellationToken in async methods
```

---

## Author notes

> All code written manually.
> Practice over reading.
> SOLID and DI are the foundation.
> Stronghold never lets down.
> All remain on Almighty.
