using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll
{
    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salaries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>(entity =>  
            {
                entity.Property(e => e.Deductions).HasColumnType("decimal(18,2)");
                entity.Property(e => e.GrossSalary).HasColumnType("decimal(18,2)");
                entity.Property(e => e.NetSalary).HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "Finance" },
                new Department { Id = 3, Name = "IT" }

            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Tanush", DepartmentId = 1, BaseSalary = 50000 },
                new Employee { Id = 2, Name = "Aditi", DepartmentId = 2, BaseSalary = 45000 },
                new Employee { Id = 3, Name = "Ravi", DepartmentId = 3, BaseSalary = 60000 }
            );

            modelBuilder.Entity<Salary>().HasData(
                new Salary
                {
                    Id = 1,
                    EmployeeId = 1,
                    GrossSalary = 50000,
                    Deductions = 0,
                    NetSalary = 50000,
                    PayDate = new DateTime(2026, 5, 1),
                    ProcessedBy = "SystemAdmin",
                    Date = new DateTime(2026, 5, 1)
                },
                new Salary
                {
                    Id = 2,
                    EmployeeId = 2,
                    GrossSalary = 45000,
                    Deductions = 0,
                    NetSalary = 45000,
                    PayDate = new DateTime(2026, 5, 1),
                    ProcessedBy = "SystemAdmin",
                    Date = new DateTime(2026, 5, 1)
                },
                new Salary
                {
                    Id = 3,
                    EmployeeId = 3,
                    GrossSalary = 60000,
                    Deductions = 0,
                    NetSalary = 60000,
                    PayDate = new DateTime(2026, 5, 1),
                    ProcessedBy = "SystemAdmin",
                    Date = new DateTime(2026, 5, 1)
                }
            );
        }
    }
}

