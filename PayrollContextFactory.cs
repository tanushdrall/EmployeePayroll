using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeePayroll
{
    public class PayrollContextFactory : IDesignTimeDbContextFactory<PayrollContext>
    {
        public PayrollContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PayrollContext>();
            optionsBuilder.UseSqlServer(
                "Server=Back-Up;Database=PayrollDb;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return new PayrollContext(optionsBuilder.Options);
        }
    }
}