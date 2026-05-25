# Employee Payroll System

A C# console application built with clean architecture.
Covers SOLID principles, async/await, dependency injection, EF Core, logging, validation, and unit tests.
All code written manually.

---

## What this app does

- Add employees via console menu
- View all employees from database
- Process payroll for all employees
- Save salary records to SQL Server
- Validate all input before processing
- Log all operations using ILogger consistently

---

## Core concepts

### SOLID
```
S  →  Employee, EmployeeService, PayrollService, SalaryCalculator — each one responsibility
O  →  New calculator = new class, existing untouched
L  →  Implementations swap via interfaces without breaking callers
I  →  IEmployeeRepository, ISalaryCalculator, IValidator — focused interfaces
D  →  All services depend on interfaces, constructor injected
```

### Dependency Injection
```
ServiceCollection    →  registers all services
BuildServiceProvider →  creates container
Transient            →  new instance per resolve
Singleton            →  one instance always
GetRequiredService   →  throws if not registered
```

### Async/Await
```
All repository methods async
SaveChangesAsync, ToListAsync, FindAsync throughout
async Task Main
No sync over async
```

### EF Core
```
PayrollContext with DbSet for Employee, Department, Salary
Code-first migrations
HasData seeding
HasColumnType decimal(18,2) for salary fields
employee.Id = 0 before insert — avoids IDENTITY_INSERT
```

### Logging
```
ILogger<T> used consistently throughout
No Console.WriteLine in production code paths
LogInformation  →  successful operations
LogWarning      →  validation failures, invalid input
LogError        →  exceptions with full context
EF Core noise filtered via AddFilter
logger declared before try block — available in catch
```

### Validation
```
IValidator<T>        →  generic interface
EmployeeValidator    →  name, salary > 0, department > 0
Validated before any DB operation
```

### Error Handling
```
try/catch in all repository and service methods
DbUpdateException caught specifically
throw after log in critical paths
Inner try/catch in payroll loop — one employee failure does not stop others
```

### Configuration
```
appsettings.json    →  connection string
Not hardcoded in source code
ConfigurationBuilder reads at startup
Excluded from source control
```

### Unit Tests
```
EmployeeValidatorTests  →  8 cases
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
├── EmployeeRepository.cs
├── SalaryRepository.cs
├── EmployeeService.cs
├── PayrollService.cs
├── SalaryCalculator.cs
├── EmployeeValidator.cs
├── PayrollContext.cs
├── PayrollContextFactory.cs
├── appsettings.json
└── Program.cs
```

---

## How to run

1. Add connection string to appsettings.json
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

info: EmployeePayroll.PayrollService[0]
      Payroll processed for Tanush, Net: 45000.00
info: EmployeePayroll.SalaryRepository[0]
      Salary saved successfully for employee Tanush
```

---

## Known limitations

```
No authentication or authorization
No role based access
Console only — no web layer
Department not validated against DB on add
No update or delete via menu
No CancellationToken in async methods
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

## Honest notes

This app was built iteratively with several correction passes.
Known recurring issues during development included inconsistent logging approaches,
swallowed exceptions, and naming inconsistencies.
These were identified and addressed but worth noting honestly.

The app works and covers the stated concepts.
Production readiness would require authentication, authorization,
proper secret management, and more thorough testing.

> All code written manually.
> Aware + Practice = Mastery.
> All remain on Almighty.
