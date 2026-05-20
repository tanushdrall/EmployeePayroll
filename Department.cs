using System.Collections.Generic;

namespace EmployeePayroll
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // ✅ Navigation property back to Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}